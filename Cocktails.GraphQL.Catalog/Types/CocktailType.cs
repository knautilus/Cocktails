using Cocktails.Data.Entities;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Catalog.Types
{
    public class CocktailType : ObjectType<Cocktail>
    {
        protected override void Configure(IObjectTypeDescriptor<Cocktail> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(x => x.CocktailCategoryId).Ignore();
            descriptor.Field(x => x.FlavorId).Ignore();

            descriptor.Field(x => x.Mixes).Type<ListType<MixType>>();
        }
    }
}
