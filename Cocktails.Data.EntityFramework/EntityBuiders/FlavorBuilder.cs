﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cocktails.Data.Domain;

namespace Cocktails.Data.EntityFramework.EntityBuiders
{
    public class FlavorBuilder : BaseBuilder<Flavor>
    {
        public FlavorBuilder(EntityTypeBuilder<Flavor> builder) : base(builder)
        {
            builder
                .Property(x => x.Name).IsRequired(true);
        }
    }
}