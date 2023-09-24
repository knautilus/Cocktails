using Cocktails.Data.Core;
using Cocktails.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cocktails.Data.EFCore.Extensions
{
    public static class LinqQueryExtensions
    {
        public static IQueryable<T> Query<T>(this DbSet<T> source, Func<IQueryable<T>, IQueryable<T>> function)
            where T : class
        {
            return function(source.AsQueryable());
        }

        public static IQueryable<T> WhereByKey<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> keySelector, TKey key)
            where T : BaseEntity<TKey>
        {
            var expression = LinqPredicates.EqualPredicate(keySelector, () => key);
            return source.Where(expression);
        }

        public static IQueryable<T> ConditionalWhere<T>(this IQueryable<T> source, bool condition,
            Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> elsePredicate = null)
        {
            return condition
                ? source.Where(predicate)
                : elsePredicate != null ? source.Where(elsePredicate) : source;
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> source,
            IPagingQuery context)
        {
            Func<IQueryable<T>, IQueryable<T>> query = x => x.Skip(context.Offset).Take(context.First);

            return query(source);
        }

        public static IQueryable<T> Sort<T, TSort>(this IQueryable<T> source,
            Expression<Func<T, TSort>> sortSelector, SortDirection sortDirection)
        {
            return source.AddOrdering(sortSelector, sortDirection);
        }

        public static IQueryable<T> Sort<T, TSort>(this IQueryable<T> source,
            (Expression<Func<T, TSort>> Selector, SortDirection Direction)[] sortings)
        {
            foreach (var sorting in sortings)
            {
                source = source.AddOrdering(sorting.Selector, sorting.Direction);
            }

            return source;
        }

        public static string ToLikePattern(this string text)
        {
            return $"%{text}%";
        }

        private static IOrderedQueryable<T> AddOrdering<T, TSort>(
            this IQueryable<T> source,
            Expression<Func<T, TSort>> sortSelector,
            SortDirection sortDirection)
        {
            var descending = sortDirection == SortDirection.Desc;

            var ordered = source as IOrderedQueryable<T>;

            // If it's not ordered yet, use OrderBy/OrderByDescending
            if (ordered == null || source.Expression.Type != typeof(IOrderedQueryable<T>))
            {
                return descending ? source.OrderByDescending(sortSelector)
                    : source.OrderBy(sortSelector);
            }

            // Already ordered, so use ThenBy/ThenByDescending
            return descending ? ordered.ThenByDescending(sortSelector)
                : ordered.ThenBy(sortSelector);
        }
    }
}
