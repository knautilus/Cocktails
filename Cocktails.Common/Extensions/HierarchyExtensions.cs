using System;
using System.Collections.Generic;

namespace Cocktails.Common.Extensions
{
    public static class HierarchyExtensions
    {
        public static IEnumerable<TSource> GetHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem,
            Func<TSource, bool> continuationFunc)
        {
            for (var current = source; continuationFunc(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        public static IEnumerable<TSource> GetHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItemFunc)
            where TSource : class
        {
            return GetHierarchy(source, nextItemFunc, s => s != null);
        }
    }
}
