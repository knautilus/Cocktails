using Cocktails.Entities.Sql;
using Cocktails.GraphQL.Core.DataLoaders;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Types
{
    public class MixType : ObjectType<Mix>
    {
        protected override void Configure(IObjectTypeDescriptor<Mix> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(x => x.Cocktail).Ignore();
            descriptor.Field(x => x.CocktailId).Ignore();
            descriptor.Field(x => x.IngredientId).Ignore();
            descriptor.Field(x => x.MeasureUnitId).Ignore();
            descriptor.Field(x => x.MeasureUnit).Type<MeasureUnitType>();

            descriptor.Field(x => x.Ingredient)
                .Resolve(x => x.DataLoader<GetByIdsDataLoader<long, Ingredient>>()
                    .LoadAsync(x.Parent<Mix>().IngredientId, new CancellationToken()));

            descriptor.Field(x => x.MeasureUnit)
                .Resolve(x => x.DataLoader<GetByIdsDataLoader<long, MeasureUnit>>()
                    .LoadAsync(x.Parent<Mix>().MeasureUnitId, new CancellationToken()));
        }
    }
}
