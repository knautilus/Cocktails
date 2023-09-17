using Cocktails.GraphQL.Cms.Queries;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Types
{
    public class MeasureUnitQueryType : ObjectTypeExtension<MeasureUnitQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<MeasureUnitQuery> descriptor)
        {
            descriptor.Field(t => t.GetMeasureUnits(default, default))
                .Argument("name", x => x.Type<StringType>())
                .UsePaging()
                .UseSorting();

            descriptor.Field(t => t.GetMeasureUnit(default, default))
                .Argument("id", x => x.Type<NonNullType<IdType>>());
        }
    }
}
