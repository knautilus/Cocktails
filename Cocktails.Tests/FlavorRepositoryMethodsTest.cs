using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

using Cocktails.Data.Domain;
using Cocktails.Data.EFCore.Options;
using Cocktails.Data.EFCore.Repositories;

namespace Cocktails.Tests
{
    [TestFixture]
    public class FlavorRepositoryMethodsTests : DbContextTest
    {
        private CancellationToken _token;
        private Repository<Flavor> _repository;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _token = new CancellationToken();
            _repository = new Repository<Flavor>(CocktailsContext, new RepositoryOptions());
        }

        [Test]
        public async Task InsertFlavorUpdatesDb()
        {
            var flavor = new Flavor { Name = "flavor1" };

            var result = await _repository.InsertAsync(flavor, _token);

            Assert.AreEqual(flavor, result);
            Assert.Contains(flavor, CocktailsContext.Flavors.ToArray());
        }

        [Test]
        public async Task UpdateFlavorUpdatesDb()
        {
            var count = CocktailsContext.Flavors.Count();
            const string newName = "new name";
            var flavor = CocktailsContext.Flavors.First();

            flavor.Name = newName;
            var result = await _repository.UpdateAsync(flavor, _token);

            Assert.AreEqual(flavor, result);
            Assert.AreEqual(count, CocktailsContext.Flavors.Count());
            Assert.Contains(flavor, CocktailsContext.Flavors.ToArray());
        }

        [Test]
        public void UpdateFlavorWithWrongId()
        {
            const string newName = "new name";
            var flavor = new Flavor { Id = Guid.NewGuid(), Name = newName };

            Assert.ThrowsAsync(typeof(DbUpdateConcurrencyException),
                async () => await _repository.UpdateAsync(flavor, _token));
        }

        [Test]
        public async Task DeleteFlavorUpdatesDb()
        {
            var count = CocktailsContext.Flavors.Count();
            var flavor = CocktailsContext.Flavors.First();
            var id = flavor.Id;

            await _repository.DeleteAsync(flavor, _token);

            Assert.AreEqual(count - 1, CocktailsContext.Flavors.Count());
            Assert.That(CocktailsContext.Flavors.All(x => x.Id != id));
        }

        [Test]
        public void DeleteFlavorWithWrongId()
        {
            var flavor = new Flavor { Id = Guid.NewGuid() };

            Assert.ThrowsAsync(typeof(DbUpdateConcurrencyException),
                async () => await _repository.DeleteAsync(flavor, _token));
        }

        [Test]
        public async Task GetFlavorWithCorrectId()
        {
            var id = Guid.NewGuid();
            var flavor = new Flavor { Id = id, Name = "flavor" };

            CocktailsContext.Add(flavor);
            CocktailsContext.SaveChanges();

            var result = await _repository.GetSingleAsync(x => x.Where(y => y.Id == id), _token);

            Assert.IsNotNull(result);
            Assert.AreEqual(flavor.Id, result.Id);
            Assert.AreEqual(flavor.Name, result.Name);
        }

        [Test]
        public async Task GetFlavorWithWrongId()
        {
            var id = Guid.NewGuid();

            var result = await _repository.GetSingleAsync(x => x.Where(y => y.Id == id), _token);

            Assert.IsNull(result);
        }
    }
}
