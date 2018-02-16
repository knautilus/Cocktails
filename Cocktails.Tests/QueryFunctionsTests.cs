using System;
using System.Linq;

using NUnit.Framework;

using Cocktails.Catalog.Services.EFCore;
using Cocktails.Data.Catalog;

namespace Cocktails.Tests
{
    [TestFixture]
    public class QueryFunctionsTests : DbContextTestBase
    {
        [Test]
        public void GetByIdTest()
        {
            var rnd = new Random();
            var index = rnd.Next(CocktailsContext.Ingredients.Count() - 1);
            var item = CocktailsContext.Ingredients.ToArray()[index];
            var id = item.Id;

            var result = QueryFunctions.GetByIdFunction<Ingredient>()(CocktailsContext.Ingredients, id).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.Contains(item, result);
        }

        [Test]
        public void GetByWrongIdTest()
        {
            var id = Guid.NewGuid();

            var result = QueryFunctions.GetByIdFunction<Ingredient>()(CocktailsContext.Ingredients, id).ToArray();
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void GetCocktailsByNameTest()
        {
            const string nameKeyword = "Name ";

            var item1 = CocktailsContext.Cocktails.ToArray()[0];
            var item2 = CocktailsContext.Cocktails.ToArray()[1];
            item1.Name = nameKeyword + 1;
            item2.Name = nameKeyword + 2;

            CocktailsContext.SaveChanges();

            var result = QueryFunctions.CocktailsByNameFunction(CocktailsContext.Cocktails, nameKeyword).ToArray();

            Assert.AreEqual(2, result.Length);
            Assert.Contains(item1, result);
            Assert.Contains(item2, result);
        }

        [Test]
        public void GetCocktailsByWrongNameTest()
        {
            const string nameKeyword = "Name ";

            var result = QueryFunctions.CocktailsByNameFunction(CocktailsContext.Cocktails, nameKeyword).ToArray();

            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void GetIngredientsByCategoryIdTest()
        {
            var rnd = new Random();
            var index = rnd.Next(CocktailsContext.Ingredients.Count() - 1);
            var item = CocktailsContext.Ingredients.ToArray()[index];
            var categoryId = item.CategoryId;

            var result = QueryFunctions.IngredientsByCategoryIdFunction(CocktailsContext.Ingredients, categoryId).ToArray();

            Assert.AreEqual(1, result.Length);
            Assert.Contains(item, result);
        }

        [Test]
        public void GetIngredientsByWrongCategoryIdTest()
        {
            var categoryId = Guid.NewGuid();

            var result = QueryFunctions.IngredientsByCategoryIdFunction(CocktailsContext.Ingredients, categoryId).ToArray();

            Assert.AreEqual(0, result.Length);
        }
    }
}
