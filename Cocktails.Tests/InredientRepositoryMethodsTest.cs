using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Data.Catalog;
using NUnit.Framework;

using Cocktails.Data.EFCore.Repositories;

namespace Cocktails.Tests
{
    [TestFixture]
    public class IngredientRepositoryMethodsTests : DbContextTestBase
    {
        private CancellationToken _token;
        private ContentRepository<Guid, Ingredient> contentRepository;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _token = new CancellationToken();
            contentRepository = new ContentRepository<Guid, Ingredient>(CocktailsContext);
        }

        [Test]
        public async Task InsertIngredientUpdatesDb()
        {
            var flavor = CocktailsContext.Flavors.First();
            var category = CocktailsContext.Categories.First();
            var ingredient = new Ingredient {
                Name = "new ingredient",
                FlavorId = flavor.Id,
                CategoryId = category.Id
            };

            var result = await contentRepository.InsertAsync(ingredient, _token);

            Assert.AreEqual(ingredient, result);
            Assert.IsNotNull(result.Flavor);
            Assert.IsNotNull(result.Category);
            Assert.Contains(ingredient, CocktailsContext.Ingredients.ToArray());
        }

        [Test]
        public async Task InsertIngredientWithWrongForeignIds()
        {
            var ingredient = new Ingredient
            {
                Name = "new ingredient",
                FlavorId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid()
            };

            var result = await contentRepository.InsertAsync(ingredient, _token);

            Assert.AreEqual(ingredient, result);
            Assert.IsNull(result.Flavor);
            Assert.IsNull(result.Category);
        }
    }
}
