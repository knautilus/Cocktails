using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Contexts;
using Cocktails.Data.EntityFramework.Options;
using Cocktails.Data.EntityFramework.Repositories;

namespace Cocktails.Tests
{
    [TestFixture]
    public class FlavorRepositoryMethodsTests
    {
        private CocktailsContext _cocktailsContext;
        private CancellationToken _token;
        private Repository<Flavor> _repository;

        [SetUp]
        public void SetUp()
        {
            InitContext();
            _token = new CancellationToken();
            _repository = new Repository<Flavor>(_cocktailsContext, new RepositoryOptions());
        }

        [Test]
        public async Task InsertFlavorUpdatesDb()
        {
            var flavor = new Flavor { Name = "flavor1" };

            var result = await _repository.InsertAsync(flavor, _token);

            Assert.AreEqual(flavor, result);
            Assert.Contains(flavor, _cocktailsContext.Flavors.ToArray());
        }

        [Test]
        public async Task UpdateFlavorUpdatesDb()
        {
            var count = _cocktailsContext.Flavors.Count();
            var newName = "new name";
            var flavor = _cocktailsContext.Flavors.First();
            var id = flavor.Id;

            flavor.Name = newName;
            var result = await _repository.UpdateAsync(flavor, _token);

            Assert.AreEqual(flavor, result);
            Assert.AreEqual(count, _cocktailsContext.Flavors.Count());
            Assert.Contains(flavor, _cocktailsContext.Flavors.ToArray());
        }

        [Test]
        public void UpdateFlavorWithWrongId()
        {
            var newName = "new name";
            var flavor = new Flavor { Id = Guid.NewGuid(), Name = newName };

            Assert.ThrowsAsync(typeof(DbUpdateConcurrencyException),
                async () => await _repository.UpdateAsync(flavor, _token));
        }

        [Test]
        public async Task DeleteFlavorUpdatesDb()
        {
            var count = _cocktailsContext.Flavors.Count();
            var flavor = _cocktailsContext.Flavors.First();
            var id = flavor.Id;

            await _repository.DeleteAsync(flavor, _token);

            Assert.AreEqual(count - 1, _cocktailsContext.Flavors.Count());
            Assert.That(_cocktailsContext.Flavors.All(x => x.Id != id));
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

            _cocktailsContext.Add(flavor);
            _cocktailsContext.SaveChanges();

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

        private void InitContext()
        {
            var builder = new DbContextOptionsBuilder<CocktailsContext>().UseInMemoryDatabase("cocktailsdb");

            _cocktailsContext = new CocktailsContext(builder.Options);

            var flavors = Enumerable.Range(1, 10)
                .Select(i => new Flavor { Id = Guid.NewGuid(), Name = $"Flavor{i}" });

            _cocktailsContext.Flavors.AddRange(flavors);

            int changed = _cocktailsContext.SaveChanges();
        }
    }
}
