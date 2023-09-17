using Cocktails.Data.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.GraphQL.Core.DataLoaders
{
    public class GetByIdsDataLoader<TKey, TEntity> : DataLoaderBase<TKey, TEntity>
        where TKey : struct
        where TEntity : BaseEntity<TKey>
    {
        private readonly DbContext _dbContext;

        public GetByIdsDataLoader(IBatchScheduler batchScheduler, DbContext dbContext) : base(batchScheduler)
        {
            _dbContext = dbContext;
        }

        protected override async ValueTask FetchAsync(IReadOnlyList<TKey> keys, Memory<Result<TEntity>> results, CancellationToken cancellationToken)
        {
            var objects = await _dbContext.Set<TEntity>().AsNoTracking()
                .Where(x => keys.Contains(x.Id))
                .Where(x => x != null)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

            for (var i = 0; i < keys.Count; i++)
            {
                if (objects.TryGetValue(keys[i], out var value))
                {
                    results.Span[i] = value;
                }
                else
                {
                    results.Span[i] = new Result<TEntity>();
                }
            }
        }
    }
}
