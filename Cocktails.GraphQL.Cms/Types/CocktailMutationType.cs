using Cocktails.GraphQL.Cms.Mutations;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Types
{
    public class CocktailMutationType : ObjectTypeExtension<CocktailMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<CocktailMutation> descriptor)
        {
            descriptor.Field(t => t.CreateCocktail(default, default, default, default))
                .Argument("request", x => x.Type<NonNullType<CocktailCreateInputType>>());

            descriptor.Field(t => t.UpdateCocktail(default, default, default, default))
                .Argument("request", x => x.Type<NonNullType<CocktailUpdateInputType>>());
        }
    }
}
