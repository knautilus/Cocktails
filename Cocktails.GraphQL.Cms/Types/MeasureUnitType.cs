using Cocktails.Entities.Sql;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Types
{
    public class MeasureUnitType : ObjectType<MeasureUnit>
    {
        protected override void Configure(IObjectTypeDescriptor<MeasureUnit> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(x => x.Mixes).Ignore();
        }
    }
}
