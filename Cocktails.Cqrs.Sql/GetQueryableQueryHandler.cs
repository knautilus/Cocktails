using Cocktails.Models.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql
{
    public class GetQueryableQueryHandler<TRequest, TEntity> : IRequestHandler<TRequest, IQueryable<TEntity>>
        where TRequest : GetQueryableQuery<TEntity>
        where TEntity : class
    {
        private readonly DbContext _dbContext;

        public GetQueryableQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected virtual Func<IQueryable<TEntity>, TRequest, IQueryable<TEntity>> Filter =>
            (source, request) => source;

        public Task<IQueryable<TEntity>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Filter(_dbContext.Set<TEntity>(), request));
        }
    }
}
