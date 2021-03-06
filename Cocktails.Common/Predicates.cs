﻿using System;
using System.Linq.Expressions;

namespace Cocktails.Common
{
    public class Predicates
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
    }
}
