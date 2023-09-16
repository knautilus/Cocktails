﻿using Cocktails.GraphQL.Catalog.Queries;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Catalog.Types
{
    public class FlavorQueryType : ObjectTypeExtension<FlavorQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<FlavorQuery> descriptor)
        {
            descriptor.Field(t => t.GetFlavors(default, default))
                .Argument("name", x => x.Type<StringType>())
                .UsePaging()
                .UseSorting();

            descriptor.Field(t => t.GetFlavor(default, default))
                .Argument("id", x => x.Type<NonNullType<IdType>>());
        }
    }
}
