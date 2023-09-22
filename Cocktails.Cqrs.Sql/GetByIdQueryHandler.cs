using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Data.EFCore.Extensions;
using Cocktails.Entities.Common;
using Cocktails.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql
{
    public class GetByIdQueryHandler<TKey, TEntity> : IQueryHandler<GetByIdQuery<TKey>, TEntity>
        where TEntity : BaseEntity<TKey>
    {
        private readonly DbContext _dbContext;

        public GetByIdQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> Handle(GetByIdQuery<TKey> request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().WhereByKey(x => x.Id, request.Id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
