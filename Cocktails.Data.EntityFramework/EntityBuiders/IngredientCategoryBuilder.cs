using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cocktails.Data.Domain;

namespace Cocktails.Data.EntityFramework.EntityBuiders
{
    public class IngredientCategoryBuilder : BaseBuilder<IngredientCategory>
    {
        public IngredientCategoryBuilder(EntityTypeBuilder<IngredientCategory> builder) : base (builder)
        {
            builder
                .Property(x => x.Name).IsRequired(true);
        }
    }
}
