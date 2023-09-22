using Cocktails.GraphQL.Cms.Queries;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Types
{
    public class FlavorQueryType : ObjectTypeExtension<FlavorQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<FlavorQuery> descriptor)
        {
            descriptor.Field(t => t.GetFlavors(default, default, default))
                .UsePaging()
                .UseSorting();

            descriptor.Field(t => t.GetFlavor(default, default, default));
        }
    }
}
