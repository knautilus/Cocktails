using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cocktails.Data.Domain;

namespace Cocktails.Data.EntityFramework.EntityBuiders
{
    public class IngredientBuilder : BaseBuilder<Ingredient>
    {
        public IngredientBuilder(EntityTypeBuilder<Ingredient> builder) : base(builder)
        {
            builder
                .HasOne(i => i.IngredientCategory)
                .WithMany(ic => ic.Ingredients)
                .HasForeignKey(i => i.IngredientCategoryId);

            builder
                .HasOne(i => i.Flavor)
                .WithMany(f => f.Ingredients)
                .HasForeignKey(i => i.FlavorId);

            builder
                .Property(x => x.Name).IsRequired(true);
        }
    }
}
