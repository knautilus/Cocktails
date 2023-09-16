using Cocktails.GraphQL.Catalog.Queries;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Catalog.Types
{
    public class CocktailQueryType : ObjectTypeExtension<CocktailQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<CocktailQuery> descriptor)
        {
            descriptor.Field(t => t.GetCocktails(default, default))
                .Argument("name", x => x.Type<StringType>())
                .Type<ListType<CocktailType>>()
                .UsePaging()
                .UseSorting();

            descriptor.Field(t => t.GetCocktail(default, default))
                .Argument("id", x => x.Type<NonNullType<IdType>>())
                .Type<CocktailType>();
        }
    }
}
