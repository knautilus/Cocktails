using Cocktails.Models.Site.Requests.Cocktails;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Site.Types
{
    public class CocktailGetManyInputType : InputObjectType<CocktailGetManyQuery>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CocktailGetManyQuery> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field(x => x.Skip).Ignore();
            descriptor.Field(x => x.Take).Ignore();
        }
    }
}
