using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Entities.Common;
using Cocktails.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql
{
    public class GetByIdsQueryHandler<TKey, TEntity> : IQueryHandler<GetByIdsQuery<TKey>, TEntity[]>
        where TEntity : BaseEntity<TKey>
    {
        private readonly DbContext _dbContext;

        public GetByIdsQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity[]> Handle(GetByIdsQuery<TKey> request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().Where(y => request.Ids.Contains(y.Id)).ToArrayAsync(cancellationToken);
        }
    }
}
