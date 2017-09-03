using Microsoft.EntityFrameworkCore;

using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.EntityBuiders;

namespace Cocktails.Data.EntityFramework.Contexts
{
    public class CocktailsContext : DbContext
    {
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Mix> Mixes { get; set; }

        public CocktailsContext(DbContextOptions<CocktailsContext> options)
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new CocktailBuilder(modelBuilder.Entity<Cocktail>());
            new FlavorBuilder(modelBuilder.Entity<Flavor>());
            new IngredientBuilder(modelBuilder.Entity<Ingredient>());
            new CategoryBuilder(modelBuilder.Entity<Category>());
            new MixBuilder(modelBuilder.Entity<Mix>());
        }
    }
}
