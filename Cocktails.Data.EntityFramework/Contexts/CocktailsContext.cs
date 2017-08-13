using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Cocktails.Common.Models;
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

        private readonly ConnectionStrings _connections;
        private readonly ILoggerFactory _loggerFactory;

        public CocktailsContext(IOptions<ConnectionStrings> connections, ILoggerFactory loggerFactory)
        {
            _connections = connections.Value;
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_connections.DefaultConnection);
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new CocktailBuilder(modelBuilder.Entity<Cocktail>());
            new FlavorBuilder(modelBuilder.Entity<Flavor>());
            new IngredientBuilder(modelBuilder.Entity<Ingredient>());
            new IngredientCategoryBuilder(modelBuilder.Entity<IngredientCategory>());
            new MixBuilder(modelBuilder.Entity<Mix>());
        }
    }
}