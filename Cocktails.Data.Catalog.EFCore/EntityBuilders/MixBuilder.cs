using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cocktails.Data.EFCore.EntityBuiders;

namespace Cocktails.Data.Catalog.EFCore.EntityBuilders
{
    public class MixBuilder : BaseContentBuilder<Mix>
    {
        public MixBuilder(EntityTypeBuilder<Mix> builder) : base(builder)
        {
            builder
                .HasKey(x => (new { x.Id, x.IngredientId }));

            builder
                .HasOne(m => m.Ingredient)
                .WithMany(i => i.Mixes)
                .HasForeignKey(m => m.IngredientId);

            builder
                .HasOne(m => m.Cocktail)
                .WithMany(c => c.Mixes)
                .HasForeignKey(m => m.Id);
        }
    }
}
