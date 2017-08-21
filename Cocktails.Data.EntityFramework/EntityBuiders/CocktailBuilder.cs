using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cocktails.Data.Domain;

namespace Cocktails.Data.EntityFramework.EntityBuiders
{
    public class CocktailBuilder : BaseBuilder<Cocktail>
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
