using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cocktails.Data.Domain;

namespace Cocktails.Data.EFCore.EntityBuiders
{
    public class CategoryBuilder : BaseBuilder<Category>
    {
        public CategoryBuilder(EntityTypeBuilder<Category> builder) : base (builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired(true);
        }
    }
}
