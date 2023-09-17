using Cocktails.GraphQL.Cms.Queries;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Types
{
    public class CocktailQueryType : ObjectTypeExtension<CocktailQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<CocktailQuery> descriptor)
        {
            descriptor.Field(t => t.GetCocktails(default, default))
                .Type<ListType<CocktailType>>()
                .UsePaging()
                .UseSorting();

            descriptor.Field(t => t.GetCocktail(default, default))
                .Argument("id", x => x.Type<NonNullType<IdType>>())
                .Type<CocktailType>();
        }
    }
}
