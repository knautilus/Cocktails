using System.Linq.Expressions;

namespace Cocktails.Data.EFCore.Extensions
{
    public static class LinqQueryExtensions
    {
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
