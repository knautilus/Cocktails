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
    class FlavorRepositoryMethodsTests
    {
        private CocktailsContext _cocktailsContext;
        private CancellationToken _token;

        [SetUp]
        public void SetUp()
        {
            InitContext();
            _token = new CancellationToken();
        }

        [Test]
        public async Task AddFlavorUpdatesDb()
        {
            var flavor = new Flavor { Name = "flavor1" };

            var repository = new Repository<Flavor>(_cocktailsContext, new RepositoryOptions());

            var result = await repository.InsertAsync(flavor, _token);

            Assert.That(_cocktailsContext.Flavors.Any(x => x.Name == flavor.Name));
        }

        [Test]
        public async Task DeleteFlavorUpdatesDb()
        {
            var flavor = _cocktailsContext.Flavors.First();
            var count = _cocktailsContext.Flavors.Count();
            var id = flavor.Id;

            var repository = new Repository<Flavor>(_cocktailsContext, new RepositoryOptions());

            await repository.DeleteAsync(flavor, _token);

            Assert.AreEqual(count - 1, _cocktailsContext.Flavors.Count());
            Assert.That(_cocktailsContext.Flavors.All(x => x.Id != id));
        }

        [Test]
        public async Task GetFlavorWithCorrectId()
        {
            var id = Guid.NewGuid();
            var flavor = new Flavor { Id = id, Name = "flavor" };

            _cocktailsContext.Add(flavor);
            _cocktailsContext.SaveChanges();

            var repository = new Repository<Flavor>(_cocktailsContext, new RepositoryOptions());

            var result = await repository.GetSingleAsync(x => x.Where(y => y.Id == id), _token);

            Assert.IsNotNull(result);
            Assert.AreEqual(flavor.Id, result.Id);
            Assert.AreEqual(flavor.Name, result.Name);
        }

        [Test]
        public async Task GetFlavorWithWrongId()
        {
            var id = Guid.NewGuid();

            var repository = new Repository<Flavor>(_cocktailsContext, new RepositoryOptions());

            var result = await repository.GetSingleAsync(x => x.Where(y => y.Id == id), _token);

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
