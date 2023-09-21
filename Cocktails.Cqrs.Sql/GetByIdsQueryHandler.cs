using Cocktails.Entities.Common;
using Cocktails.Models.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql
{
    public class GetByIdsQueryHandler<TKey, TEntity> : IRequestHandler<GetByIdsQuery<TKey, TEntity>, TEntity[]>
        where TEntity : BaseEntity<TKey>
    {
        private readonly DbContext _dbContext;

        public GetByIdsQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity[]> Handle(GetByIdsQuery<TKey, TEntity> request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().Where(y => request.Ids.Contains(y.Id)).ToArrayAsync(cancellationToken);
        }
    }
}
