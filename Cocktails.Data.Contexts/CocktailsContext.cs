using Cocktails.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Data.Contexts
{
    public class CocktailsContext : DbContext
    {
        public CocktailsContext(DbContextOptions<CocktailsContext> options)
            : base(options)
        {
        }

        protected CocktailsContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(b =>
            {
                b.Property(x => x.Name)
                    .HasMaxLength(128)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Flavor>(b =>
            {
                b.Property(x => x.Name)
                    .HasMaxLength(128)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Ingredient>(b =>
            {
                b.Property(x => x.Name)
                    .HasMaxLength(128)
                    .IsRequired(true);

                b.HasOne(i => i.Category)
                   .WithMany(ic => ic.Ingredients)
                   .HasForeignKey(i => i.CategoryId);

                b.HasOne(i => i.Flavor)
                    .WithMany(f => f.Ingredients)
                    .HasForeignKey(i => i.FlavorId);
            });

            modelBuilder.Entity<Cocktail>(b =>
            {
                b.Property(x => x.Name)
                    .HasMaxLength(128)
                    .IsRequired(true);

                b.Property(x => x.Description)
                    .HasMaxLength(2000);
            });

            modelBuilder.Entity<Mix>(b =>
            {
                b.HasKey(x => (new { x.Id, x.IngredientId }));

                b.HasOne(m => m.Ingredient)
                    .WithMany(i => i.Mixes)
                    .HasForeignKey(m => m.IngredientId);

                b.HasOne(m => m.Cocktail)
                    .WithMany(c => c.Mixes)
                    .HasForeignKey(m => m.Id);
            });
        }
    }
}