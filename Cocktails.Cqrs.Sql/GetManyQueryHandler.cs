using Cocktails.Models.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Cocktails.Data.EFCore.Extensions;

namespace Cocktails.Cqrs.Sql
{
    public class GetManyQueryHandler<TEntity, TGetManyQuery, TSortFieldEnum> : IRequestHandler<TGetManyQuery, TEntity[]>
        where TEntity : class
        where TGetManyQuery : GetManyQuery<TEntity, TSortFieldEnum>
        where TSortFieldEnum : struct
    {
        protected readonly DbContext ReadDbContext;

        public GetManyQueryHandler(DbContext readDbContext)
        {
            ReadDbContext = readDbContext;
        }

        protected Func<IQueryable<TEntity>, TGetManyQuery, IQueryable<TEntity>> SortAndPaginate =>
            (x, query) => Sort(x, query)
                .Paginate(query);

        protected virtual Func<IQueryable<TEntity>, TGetManyQuery, IQueryable<TEntity>> Filter =>
            (x, query) => x;

        protected virtual Func<IQueryable<TEntity>, TGetManyQuery, IQueryable<TEntity>> Include =>
            (x, query) => x;

        protected virtual Func<IQueryable<TEntity>, TGetManyQuery, IQueryable<TEntity>> Sort =>
            (x, query) => x
                .Sort(GetSortSelector(query.SortField), query.SortDirection);

        public virtual async Task<TEntity[]> Handle(TGetManyQuery request, CancellationToken cancellationToken)
        {
            var result = await ReadDbContext.Set<TEntity>()
                .Query(x => Include(SortAndPaginate(Filter(x, request), request), request))
                .ToArrayAsync(cancellationToken);

            ReadDbContext.ChangeTracker.Clear();

            return result;
        }

        protected virtual Expression<Func<TEntity, object>> GetSortSelector(TSortFieldEnum sortField)
        {
            return element => element;
        }
    }
}
