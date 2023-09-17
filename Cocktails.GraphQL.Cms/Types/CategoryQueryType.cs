using Cocktails.GraphQL.Cms.Queries;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Types
{
    public class CategoryQueryType : ObjectTypeExtension<CategoryQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<CategoryQuery> descriptor)
        {
            descriptor.Field(t => t.GetCategories(default, default, default))
                .Argument("name", x => x.Type<StringType>())
                .UsePaging()
                .UseSorting();

            descriptor.Field(t => t.GetCategory(default, default, default))
                .Argument("id", x => x.Type<NonNullType<IdType>>());
        }
    }
}
