using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Cocktails.Data.Domain;

namespace Cocktails.Data.EntityFramework.EntityBuiders
{
    public class BaseBuilder<T> where T : BaseEntity
    {
        public BaseBuilder(EntityTypeBuilder<T> builder)
        {
            builder
                .Property(b => b.CreatedDate)
                .HasComputedColumnSql("SYSUTCDATETIME()");

            builder
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("SYSUTCDATETIME()");
        }
    }
}
