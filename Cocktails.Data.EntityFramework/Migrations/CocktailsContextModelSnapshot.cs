using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Cocktails.Data.EntityFramework.Contexts;

namespace Cocktails.Data.EntityFramework.Migrations
{
    [DbContext(typeof(CocktailsContext))]
    partial class CocktailsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cocktails.Data.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("SYSUTCDATETIME()");

                    b.Property<DateTimeOffset>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSUTCDATETIME()");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Cocktails.Data.Domain.Cocktail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("SYSUTCDATETIME()");

                    b.Property<string>("Description");

                    b.Property<DateTimeOffset>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSUTCDATETIME()");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Cocktails");
                });

            modelBuilder.Entity("Cocktails.Data.Domain.Flavor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("SYSUTCDATETIME()");

                    b.Property<DateTimeOffset>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSUTCDATETIME()");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Flavors");
                });

            modelBuilder.Entity("Cocktails.Data.Domain.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CategoryId");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("SYSUTCDATETIME()");

                    b.Property<Guid>("FlavorId");

                    b.Property<DateTimeOffset>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSUTCDATETIME()");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FlavorId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Cocktails.Data.Domain.Mix", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("IngredientId");

                    b.Property<decimal>("Amount");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("SYSUTCDATETIME()");

                    b.Property<DateTimeOffset>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSUTCDATETIME()");

                    b.HasKey("Id", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("Mixes");
                });

            modelBuilder.Entity("Cocktails.Data.Domain.Ingredient", b =>
                {
                    b.HasOne("Cocktails.Data.Domain.Category", "Category")
                        .WithMany("Ingredients")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Cocktails.Data.Domain.Flavor", "Flavor")
                        .WithMany("Ingredients")
                        .HasForeignKey("FlavorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Cocktails.Data.Domain.Mix", b =>
                {
                    b.HasOne("Cocktails.Data.Domain.Cocktail", "Cocktail")
                        .WithMany("Mixes")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Cocktails.Data.Domain.Ingredient", "Ingredient")
                        .WithMany("Mixes")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
