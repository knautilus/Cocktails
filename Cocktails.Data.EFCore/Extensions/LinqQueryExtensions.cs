using Cocktails.Entities.Common;
using System.Linq.Expressions;

namespace Cocktails.Data.EFCore.Extensions
{
    public static class LinqQueryExtensions
    {
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

        public static string ToLikePattern(this string text)
        {
            return $"%{text}%";
        }
    }
}
