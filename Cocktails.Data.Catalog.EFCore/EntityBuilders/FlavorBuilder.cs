using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cocktails.Data.EFCore.EntityBuiders;

namespace Cocktails.Data.Catalog.EFCore.EntityBuilders
{
    public class FlavorBuilder : BaseContentBuilder<Flavor>
    {
        public FlavorBuilder(EntityTypeBuilder<Flavor> builder) : base(builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);
        }
    }
}
