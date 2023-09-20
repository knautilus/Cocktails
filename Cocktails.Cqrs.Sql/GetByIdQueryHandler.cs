using Cocktails.Data.EFCore.Extensions;
using Cocktails.Entities.Common;
using Cocktails.Models.Cms.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql
{
    public class GetByIdQueryHandler<TKey, TEntity> : IRequestHandler<GetByIdQuery<TKey, TEntity>, TEntity>
        where TEntity : BaseEntity<TKey>
    {
        private readonly DbContext _dbContext;

        public GetByIdQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> Handle(GetByIdQuery<TKey, TEntity> request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().WhereByKey(x => x.Id, request.Id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
