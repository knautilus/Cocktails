using System;
using System.Linq;
using Cocktails.Data.Catalog;
using Cocktails.Data.Catalog.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Cocktails.Tests
{
    public abstract class DbContextTestBase
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

        private void InitContext()
        {
            var rand = new Random();
            CocktailsContext = new CocktailsContext(CreateContextOptions());

            var flavors = Enumerable.Range(1, 100)
                .Select(i => new Flavor
                {
                    Id = Guid.NewGuid(),
                    Name = $"Flavor{i}",
                    ModifiedDate = DateTimeOffset.UtcNow.AddTicks(rand.Next())
                });

            var categories = Enumerable.Range(1, 100)
                .Select(i => new Category
                {
                    Id = Guid.NewGuid(),
                    Name = $"Category{i}",
                    ModifiedDate = DateTimeOffset.UtcNow.AddTicks(rand.Next())
                });

            CocktailsContext.Flavors.AddRange(flavors);
            CocktailsContext.Categories.AddRange(categories);

            CocktailsContext.SaveChanges();

            var ingredients = Enumerable.Range(1, 100)
                .Select(i => new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = $"Ingredient{i}",
                    FlavorId = CocktailsContext.Flavors.ToArray()[i - 1].Id,
                    CategoryId = CocktailsContext.Categories.ToArray()[i - 1].Id,
                    ModifiedDate = DateTimeOffset.UtcNow.AddTicks(rand.Next())
                });

            CocktailsContext.Ingredients.AddRange(ingredients);

            CocktailsContext.SaveChanges();

            var cocktails = Enumerable.Range(1, 100)
                .Select(i => new Cocktail
                {
                    Id = Guid.NewGuid(),
                    Name = $"Cocktail{i}",
                    ModifiedDate = DateTimeOffset.UtcNow.AddTicks(rand.Next())
                });

            CocktailsContext.Cocktails.AddRange(cocktails);

            CocktailsContext.SaveChanges();
        }

        private static DbContextOptions<CocktailsContext> CreateContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<CocktailsContext>();
            builder.UseInMemoryDatabase("cocktailsdb")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
