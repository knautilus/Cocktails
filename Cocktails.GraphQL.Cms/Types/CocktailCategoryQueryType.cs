﻿using Cocktails.GraphQL.Cms.Queries;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Types
{
    public class CocktailCategoryQueryType : ObjectTypeExtension<CocktailCategoryQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<CocktailCategoryQuery> descriptor)
        {
            descriptor.Field(t => t.GetCategories(default, default, default))
                .Argument("name", x => x.Type<StringType>())
                .UsePaging()
                .UseSorting();

            descriptor.Field(t => t.GetCategory(default, default, default))
                .Argument("id", x => x.Type<NonNullType<IdType>>());
        }
    }
}