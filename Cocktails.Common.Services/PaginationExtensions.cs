using System;
using System.Linq;
using System.Linq.Expressions;
using Cocktails.Common.Models;

namespace Cocktails.Common.Services
{
    public static class PaginationExtensions
    {
        public static IQueryable<TElement> Paginate<TElement>(this IQueryable<TElement> source, QueryContext context, Expression<Func<TElement, DateTimeOffset>> sortSelector)
        {
            Func<IQueryable<TElement>, IOrderedQueryable<TElement>> resultSortFunction;
            Func<IQueryable<TElement>, IOrderedQueryable<TElement>> sortFunction;
            Func<IQueryable<TElement>, IQueryable<TElement>> cursorFunction;
            if (context.Sort == SortMode.Asc)
            {
                resultSortFunction = x => x.OrderBy(sortSelector);
                if (context.BeforeDate.HasValue)
                {
                    sortFunction = x => x.OrderByDescending(sortSelector);
                    cursorFunction = x => x.Where(Predicates.LessThanPredicate(sortSelector, () => context.BeforeDate.Value));
                }
                else if (context.AfterDate.HasValue)
                {
                    sortFunction = x => x.OrderBy(sortSelector);
                    cursorFunction = x => x.Where(Predicates.GreaterThanPredicate(sortSelector, () => context.AfterDate.Value));
                }
                else
                {
                    sortFunction = x => x.OrderBy(sortSelector);
                    cursorFunction = x => x;
                }
            }
            else
            {
                resultSortFunction = x => x.OrderByDescending(sortSelector);
                if (context.BeforeDate.HasValue)
                {
                    sortFunction = x => x.OrderBy(sortSelector);
                    cursorFunction = x => x.Where(Predicates.GreaterThanPredicate(sortSelector, () => context.BeforeDate.Value));
                }
                else if (context.AfterDate.HasValue)
                {
                    sortFunction = x => x.OrderByDescending(sortSelector);
                    cursorFunction = x => x.Where(Predicates.LessThanPredicate(sortSelector, () => context.AfterDate.Value));
                }
                else
                {
                    sortFunction = x => x.OrderByDescending(sortSelector);
                    cursorFunction = x => x;
                }
            }
            Func<IQueryable<TElement>, IQueryable<TElement>> query = x => sortFunction(cursorFunction(x)).Take(context.Limit);
            if (context.BeforeDate.HasValue)
            {
                return resultSortFunction(query(source));
            }
            return query(source);
        }
    }
}
