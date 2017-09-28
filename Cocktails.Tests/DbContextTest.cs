using System;
using System.Linq;

using NUnit.Framework;

using Cocktails.Data.Domain;
using Cocktails.Data.EFCore.Contexts;

namespace Cocktails.Tests
{
    public abstract class DbContextTest
    {
        protected CocktailsContext CocktailsContext;

        [SetUp]
        public virtual void SetUp()
        {
            InitContext();
        }

        [TearDown]
        public virtual void TearDown()
        {
            CocktailsContext.Dispose();
        }

        protected virtual void InitContext()
        {
            CocktailsContext = new CocktailsContext(TestsHelper.CreateContextOptions());

            var flavors = Enumerable.Range(1, 10)
                .Select(i => new Flavor
                {
                    Id = Guid.NewGuid(),
                    Name = $"Flavor{i}"
                });

            var categories = Enumerable.Range(1, 10)
                .Select(i => new Category
                {
                    Id = Guid.NewGuid(),
                    Name = $"Category{i}"
                });

            CocktailsContext.Flavors.AddRange(flavors);
            CocktailsContext.Categories.AddRange(categories);

            CocktailsContext.SaveChanges();

            var ingredients = Enumerable.Range(1, 10)
                .Select(i => new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = $"Ingredient{i}",
                    FlavorId = CocktailsContext.Flavors.ToArray()[i - 1].Id,
                    CategoryId = CocktailsContext.Categories.ToArray()[i - 1].Id
                });

            CocktailsContext.Ingredients.AddRange(ingredients);

            CocktailsContext.SaveChanges();

            var cocktails = Enumerable.Range(1, 10)
                .Select(i => new Cocktail
                {
                    Id = Guid.NewGuid(),
                    Name = $"Cocktail{i}"
                });

            CocktailsContext.Cocktails.AddRange(cocktails);

            CocktailsContext.SaveChanges();
        }
    }
}
