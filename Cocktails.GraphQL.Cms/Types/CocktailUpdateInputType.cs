using Cocktails.Models.Cms.Requests.Cocktail;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Types
{
    public class CocktailUpdateInputType : InputObjectType<CocktailUpdateCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CocktailUpdateCommand> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field(f => f.ModifyUserId).Ignore();
            descriptor.Field(f => f.ModifyDate).Ignore();
            descriptor.Field(x => x.Name).Type<NonNullType<StringType>>();
        }
    }
}
