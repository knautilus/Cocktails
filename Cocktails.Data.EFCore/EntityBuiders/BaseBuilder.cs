using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Cocktails.Data.Domain;

namespace Cocktails.Data.EFCore.EntityBuiders
{
    public class BaseContentBuilder<T> where T : BaseContentEntity
    {
        public BaseContentBuilder(EntityTypeBuilder<T> builder)
        {
            builder
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .ValueGeneratedOnAddOrUpdate();

            builder
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("SYSUTCDATETIME()");
        }
    }
}
