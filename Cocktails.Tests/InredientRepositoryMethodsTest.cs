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
    class IngredientRepositoryMethodsTests
    {
        private CocktailsContext _cocktailsContext;
        private CancellationToken _token;
        private Repository<Ingredient> _repository;

        [SetUp]
        public void SetUp()
        {
            InitContext();
            _token = new CancellationToken();
            _repository = new Repository<Ingredient>(_cocktailsContext, new RepositoryOptions());
        }

        [Test]
        public async Task InsertIngredientUpdatesDb()
        {
            var flavor = _cocktailsContext.Flavors.First();
            var category = _cocktailsContext.Categories.First();
            var ingredient = new Ingredient {
                Name = "new ingredient",
                FlavorId = flavor.Id,
                CategoryId = category.Id
            };

            var result = await _repository.InsertAsync(ingredient, _token);

            Assert.AreEqual(ingredient, result);
            Assert.IsNotNull(result.Flavor);
            Assert.IsNotNull(result.Category);
            Assert.Contains(ingredient, _cocktailsContext.Ingredients.ToArray());
        }

        [Test]
        public async Task InsertIngredientWithWrongForeignIds()
        {
            var flavor = _cocktailsContext.Flavors.First();
            var category = _cocktailsContext.Categories.First();
            var ingredient = new Ingredient
            {
                Name = "new ingredient",
                FlavorId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid()
            };

            var result = await _repository.InsertAsync(ingredient, _token);

            Assert.AreEqual(ingredient, result);
            Assert.IsNull(result.Flavor);
            Assert.IsNull(result.Category);
        }

        private void InitContext()
        {
            var builder = new DbContextOptionsBuilder<CocktailsContext>().UseInMemoryDatabase("cocktailsdb");

            _cocktailsContext = new CocktailsContext(builder.Options);

            var flavors = Enumerable.Range(1, 10)
                .Select(i => new Flavor {
                    Id = Guid.NewGuid(), Name = $"Flavor{i}" });

            var categories = Enumerable.Range(1, 10)
                .Select(i => new Category {
                    Id = Guid.NewGuid(), Name = $"Category{i}" });

            _cocktailsContext.Flavors.AddRange(flavors);
            _cocktailsContext.Categories.AddRange(categories);

            _cocktailsContext.SaveChanges();

            var ingredients = Enumerable.Range(1, 10)
                .Select(i => new Ingredient {
                    Id = Guid.NewGuid(),
                    Name = $"Ingredient{i}",
                    FlavorId = _cocktailsContext.Flavors.ToArray()[i-1].Id,
                    CategoryId = _cocktailsContext.Categories.ToArray()[i-1].Id
                });

            _cocktailsContext.Ingredients.AddRange(ingredients);

            int changed = _cocktailsContext.SaveChanges();
        }
    }
}
