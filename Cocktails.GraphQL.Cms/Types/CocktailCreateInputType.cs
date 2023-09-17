using Cocktails.Models.Cms.Requests.Cocktails;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Types
{
    public class CocktailCreateInputType : InputObjectType<CocktailCreateCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CocktailCreateCommand> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field(f => f.CreateUserId).Ignore();
            descriptor.Field(f => f.CreateDate).Ignore();
            descriptor.Field(f => f.ModifyUserId).Ignore();
            descriptor.Field(f => f.ModifyDate).Ignore();
            descriptor.Field(f => f.Id).Ignore();
            descriptor.Field(x => x.Name).Type<NonNullType<StringType>>();
        }
    }
}
