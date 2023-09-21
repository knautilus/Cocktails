using Cocktails.GraphQL.Site.Queries;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Site.Types
{
    public class CocktailQueryType : ObjectTypeExtension<CocktailQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<CocktailQuery> descriptor)
        {
            descriptor.Field(t => t.GetCocktails(default, default, default))
                .Type<ListType<CocktailType>>();

            //descriptor.Field(t => t.GetCocktail(default, default, default))
            //    .Type<CocktailType>();
        }
    }
}
