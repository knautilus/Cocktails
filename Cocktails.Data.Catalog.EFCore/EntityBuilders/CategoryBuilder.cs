using Cocktails.Data.EFCore.EntityBuiders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cocktails.Data.Catalog.EFCore.EntityBuilders
{
    public class CategoryBuilder : BaseContentBuilder<Category>
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
