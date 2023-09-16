using Cocktails.Data.Entities;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Catalog.Types
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
