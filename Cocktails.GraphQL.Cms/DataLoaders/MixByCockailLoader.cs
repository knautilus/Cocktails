using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Mixes;
using GreenDonut;
using HotChocolate;
using MediatR;

namespace Cocktails.GraphQL.Cms.DataLoaders
{
    public class MixByCockailLoader : DataLoaderBase<long, Mix[]>
    {
        private readonly IMediator _queryProcessor;

        public MixByCockailLoader(IBatchScheduler batchScheduler, [Service] IMediator queryProcessor) : base(batchScheduler)
        {
            _queryProcessor = queryProcessor;
        }

        protected override async ValueTask FetchAsync(IReadOnlyList<long> keys, Memory<Result<Mix[]>> results, CancellationToken cancellationToken)
        {
            var objects = (await _queryProcessor.Send(new MixesGetByCocktailIdsQuery { CocktailIds = keys.ToArray() }, cancellationToken))
                .ToLookup(x => x.CocktailId);

            for (int i = 0; i < keys.Count; i++)
            {
                results.Span[i] = objects[keys[i]].ToArray();
            }
        }
    }
}
