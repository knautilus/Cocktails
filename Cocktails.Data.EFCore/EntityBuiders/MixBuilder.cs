﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cocktails.Data.Domain;

namespace Cocktails.Data.EFCore.EntityBuiders
{
    public class MixBuilder : BaseBuilder<Mix>
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
