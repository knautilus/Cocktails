using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cocktails.Data.Domain;

namespace Cocktails.Data.EFCore.EntityBuiders
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
