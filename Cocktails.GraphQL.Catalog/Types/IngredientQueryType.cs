using Cocktails.GraphQL.Catalog.Queries;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Catalog.Types
{
    public class IngredientQueryType : ObjectTypeExtension<IngredientQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<IngredientQuery> descriptor)
        {
            descriptor.Field(t => t.GetIngredients(default, default))
                .Argument("name", x => x.Type<StringType>())
                .UsePaging()
                .UseSorting();

            descriptor.Field(t => t.GetIngredient(default, default))
                .Argument("id", x => x.Type<NonNullType<IdType>>());
        }
    }
}
