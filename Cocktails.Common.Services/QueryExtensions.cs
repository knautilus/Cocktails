using System;
using System.Linq;
using System.Linq.Expressions;
using Cocktails.Data;

namespace Cocktails.Common.Services
{
    public static class QueryExtensions
    {
        public static IQueryable<T> WhereByKey<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> keySelector, TKey key)
            where TKey : struct
            where T : BaseEntity<TKey>
        {
            var expression = Predicates.EqualPredicate(keySelector, () => key);
            return source.Where(expression);
        }
    }
}
