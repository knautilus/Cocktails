using System.Linq.Expressions;

namespace Cocktails.Data.EFCore
{
    public class LinqPredicates
    {
        public static Expression<Func<T, bool>> GreaterThanPredicate<T, TValue>(Expression<Func<T, TValue>> selector, Expression<Func<TValue>> value)
        {
            var expression = Expression.Lambda<Func<T, bool>>(
                Expression.GreaterThan(selector.Body, value.Body),
                selector.Parameters);
            return expression;
        }

        public static Expression<Func<T, bool>> LessThanPredicate<T, TValue>(Expression<Func<T, TValue>> selector, Expression<Func<TValue>> value)
        {
            var expression = Expression.Lambda<Func<T, bool>>(
                Expression.LessThan(selector.Body, value.Body),
                selector.Parameters);
            return expression;
        }

        public static Expression<Func<T, bool>> EqualPredicate<T, TValue>(Expression<Func<T, TValue>> selector, Expression<Func<TValue>> value)
        {
            var expression = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(selector.Body, value.Body),
                selector.Parameters);
            return expression;
        }
    }
}
