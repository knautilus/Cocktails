using Cocktails.Entities.Sql;
using Cocktails.GraphQL.Cms.DataLoaders;
using Cocktails.GraphQL.Core.DataLoaders;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Types
{
    public class CocktailType : ObjectType<Cocktail>
    {
        protected override void Configure(IObjectTypeDescriptor<Cocktail> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(x => x.CocktailCategoryId).Ignore();
            descriptor.Field(x => x.FlavorId).Ignore();

            descriptor.Field(x => x.Mixes).Type<ListType<MixType>>();

            descriptor.Field(x => x.CocktailCategory)
                .Resolve(x => x.DataLoader<GetByIdsDataLoader<long, CocktailCategory>>()
                    .LoadAsync(x.Parent<Cocktail>().CocktailCategoryId, new CancellationToken()));

            descriptor.Field(x => x.Flavor)
                .Resolve(x => x.DataLoader<GetByIdsDataLoader<long, Flavor>>()
                    .LoadAsync(x.Parent<Cocktail>().FlavorId, new CancellationToken()));

            descriptor.Field(x => x.Mixes)
                .Resolve(x => x.DataLoader<MixByCockailLoader>()
                    .LoadAsync(x.Parent<Cocktail>().Id, new CancellationToken()));
        }
    }
}
