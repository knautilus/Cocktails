using Cocktails.Data.Contexts;
using Cocktails.Data.Entities;
using GreenDonut;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.GraphQL.Cms.DataLoaders
{
    public class MixByCockailLoader : DataLoaderBase<long, Mix[]>
    {
        private readonly CocktailsContext _cocktailsContext;

        public MixByCockailLoader(IBatchScheduler batchScheduler, CocktailsContext cocktailsContext) : base(batchScheduler)
        {
            _cocktailsContext = cocktailsContext;
        }

        protected override async ValueTask FetchAsync(IReadOnlyList<long> keys, Memory<Result<Mix[]>> results, CancellationToken cancellationToken)
        {
            var objects = (await _cocktailsContext.Set<Mix>().AsNoTracking()
                .Where(x => keys.Contains(x.CocktailId))
                .Where(x => x != null)
                .ToArrayAsync(cancellationToken))
                .ToLookup(x => x.CocktailId);

            for (int i = 0; i < keys.Count; i++)
            {
                results.Span[i] = objects[keys[i]].ToArray();
            }
        }
    }
}
