using Cocktails.Data.Entities;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Catalog.Types
{
    public class MixType : ObjectType<Mix>
    {
        protected override void Configure(IObjectTypeDescriptor<Mix> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(x => x.Cocktail).Ignore();
            descriptor.Field(x => x.Id).Ignore();
            descriptor.Field(x => x.IngredientId).Ignore();
            descriptor.Field(x => x.MeasureUnitId).Ignore();
            descriptor.Field(x => x.MeasureUnit).Type<MeasureUnitType>();
        }
    }
}
