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
        public DbSet<IngredientCategory> IngredientCategories { get; set; }
        public DbSet<Mix> Mixes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=cocktailsdb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CocktailBuilder(modelBuilder.Entity<Cocktail>());
            new FlavorBuilder(modelBuilder.Entity<Flavor>());
            new IngredientBuilder(modelBuilder.Entity<Ingredient>());
            new IngredientCategoryBuilder(modelBuilder.Entity<IngredientCategory>());
            new MixBuilder(modelBuilder.Entity<Mix>());
        }
    }
}