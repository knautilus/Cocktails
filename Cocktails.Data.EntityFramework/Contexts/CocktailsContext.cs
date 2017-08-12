using Microsoft.EntityFrameworkCore;

using Cocktails.Data.Domain;

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
            ConfigureCocktail(modelBuilder);
            ConfigureFlavor(modelBuilder);
            ConfigureIngredient(modelBuilder);
            ConfigureIngredientCategory(modelBuilder);
            ConfigureMix(modelBuilder);
        }

        private static void ConfigureCocktail(ModelBuilder modelBuilder)
        {
            var cocktailEntity = modelBuilder.Entity<Cocktail>();

            cocktailEntity
                .Property(x => x.Name).IsRequired(true);
        }

        private static void ConfigureFlavor(ModelBuilder modelBuilder)
        {
            var flavorEntity = modelBuilder.Entity<Flavor>();

            flavorEntity
                .Property(x => x.Name).IsRequired(true);
        }

        private static void ConfigureIngredient(ModelBuilder modelBuilder)
        {
            var ingredientEntity = modelBuilder.Entity<Ingredient>();

            ingredientEntity
                .HasOne(i => i.IngredientCategory)
                .WithMany(ic => ic.Ingredients)
                .HasForeignKey(i => i.IngredientCategoryId);

            ingredientEntity
                .HasOne(i => i.Flavor)
                .WithMany(f => f.Ingredients)
                .HasForeignKey(i => i.FlavorId);

            ingredientEntity
                .Property(x => x.Name).IsRequired(true);
        }

        private static void ConfigureIngredientCategory(ModelBuilder modelBuilder)
        {
            var ingredientCategoryEntity = modelBuilder.Entity<IngredientCategory>();

            ingredientCategoryEntity
                .Property(x => x.Name).IsRequired(true);
        }

        private static void ConfigureMix(ModelBuilder modelBuilder)
        {
            var mixEntity = modelBuilder.Entity<Mix>();

            mixEntity
                .HasKey(x => (new { x.CocktailId, x.IngredientId }));

            mixEntity
                .HasOne(m => m.Ingredient)
                .WithMany(i => i.Mixes)
                .HasForeignKey(m => m.IngredientId);

            mixEntity
                .HasOne(m => m.Cocktail)
                .WithMany(c => c.Mixes)
                .HasForeignKey(m => m.CocktailId);
        }
    }
}