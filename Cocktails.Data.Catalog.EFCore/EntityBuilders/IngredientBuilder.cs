using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cocktails.Data.EFCore.EntityBuiders;

namespace Cocktails.Data.Catalog.EFCore.EntityBuilders
{
    public class IngredientBuilder : BaseContentBuilder<Ingredient>
    {
        public IngredientBuilder(EntityTypeBuilder<Ingredient> builder) : base(builder)
        {
            builder
                .HasOne(i => i.Category)
                .WithMany(ic => ic.Ingredients)
                .HasForeignKey(i => i.CategoryId);

            builder
                .HasOne(i => i.Flavor)
                .WithMany(f => f.Ingredients)
                .HasForeignKey(i => i.FlavorId);

            builder
                .Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);
        }
    }
}
