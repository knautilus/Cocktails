using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cocktails.Data.EFCore.EntityBuiders;

namespace Cocktails.Data.Catalog.EFCore.EntityBuilders
{
    public class CocktailBuilder : BaseContentBuilder<Cocktail>
    {
        public CocktailBuilder(EntityTypeBuilder<Cocktail> builder) : base(builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);

            builder
                .Property(x => x.Description)
                .HasMaxLength(2000);
        }
    }
}
