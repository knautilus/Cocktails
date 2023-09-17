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
            modelBuilder.Entity<CocktailCategory>(b =>
            {
                b.HasKey(x => new { x.Id });

                b.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                b.Property(x => x.Name)
                    .HasMaxLength(128)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Flavor>(b =>
            {
                b.HasKey(x => new { x.Id });

                b.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                b.Property(x => x.Name)
                    .HasMaxLength(128)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Ingredient>(b =>
            {
                b.HasKey(x => new { x.Id });

                b.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                b.Property(x => x.Name)
                    .HasMaxLength(128)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Cocktail>(b =>
            {
                b.HasKey(x => new { x.Id });

                b.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                b.Property(x => x.Name)
                    .HasMaxLength(128)
                    .IsRequired(true);

                b.HasOne(x => x.CocktailCategory)
                   .WithMany(cc => cc.Cocktails)
                   .HasForeignKey(x => x.CocktailCategoryId);

                b.HasOne(x => x.Flavor)
                    .WithMany(f => f.Cocktails)
                    .HasForeignKey(x => x.FlavorId);

                b.Property(x => x.Description)
                    .HasMaxLength(2000);
            });

            modelBuilder.Entity<Mix>(b =>
            {
                b.HasKey(x => (new { x.CocktailId, x.IngredientId }));

                b.HasOne(x => x.Ingredient)
                    .WithMany(i => i.Mixes)
                    .HasForeignKey(x => x.IngredientId);

                b.HasOne(x => x.Cocktail)
                    .WithMany(c => c.Mixes)
                    .HasForeignKey(x => x.CocktailId);

                b.HasOne(x => x.MeasureUnit)
                    .WithMany(mu => mu.Mixes)
                    .HasForeignKey(x => x.MeasureUnitId);
            });

            FillData(modelBuilder);
        }

        private static void FillData(ModelBuilder modelBuilder)
        {
            var date = DateTimeOffset.UtcNow;

            modelBuilder.Entity<Flavor>().HasData(
                new Flavor { Id = 1, Name = "Bitter", CreateDate = date, ModifyDate = date },
                new Flavor { Id = 2, Name = "Sweet", CreateDate = date, ModifyDate = date },
                new Flavor { Id = 3, Name = "Sour", CreateDate = date, ModifyDate = date },
                new Flavor { Id = 4, Name = "Fruity", CreateDate = date, ModifyDate = date }
            );

            modelBuilder.Entity<CocktailCategory>().HasData(
                new CocktailCategory { Id = 1, Name = "Classic", CreateDate = date, ModifyDate = date },
                new CocktailCategory { Id = 2, Name = "Modern classic", CreateDate = date, ModifyDate = date },
                new CocktailCategory { Id = 3, Name = "Coffee & Dessert", CreateDate = date, ModifyDate = date },
                new CocktailCategory { Id = 4, Name = "Shots", CreateDate = date, ModifyDate = date },
                new CocktailCategory { Id = 5, Name = "Tropical", CreateDate = date, ModifyDate = date },
                new CocktailCategory { Id = 6, Name = "Nonalcoholic", CreateDate = date, ModifyDate = date }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Vodka", CreateDate = date, ModifyDate = date },
                new Ingredient { Id = 2, Name = "Gin", CreateDate = date, ModifyDate = date },
                new Ingredient { Id = 3, Name = "Rum", CreateDate = date, ModifyDate = date },
                new Ingredient { Id = 4, Name = "Tequila", CreateDate = date, ModifyDate = date },
                new Ingredient { Id = 5, Name = "Tomato juice", CreateDate = date, ModifyDate = date },
                new Ingredient { Id = 6, Name = "Lemon juice", CreateDate = date, ModifyDate = date },
                new Ingredient { Id = 7, Name = "Coffee liqueur", CreateDate = date, ModifyDate = date }
            );

            modelBuilder.Entity<MeasureUnit>().HasData(
                new MeasureUnit { Id = 1, Name = "Oz", CreateDate = date, ModifyDate = date },
                new MeasureUnit { Id = 2, Name = "Piece", CreateDate = date, ModifyDate = date },
                new MeasureUnit { Id = 3, Name = "Dash", CreateDate = date, ModifyDate = date },
                new MeasureUnit { Id = 4, Name = "Cup", CreateDate = date, ModifyDate = date },
                new MeasureUnit { Id = 5, Name = "Shot", CreateDate = date, ModifyDate = date }
            );
        }
    }
}