using Cocktails.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cocktails.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, TKey> getKeyFunc)
        {
            return items
                .GroupJoin(other, getKeyFunc, getKeyFunc, (item, tempItems) => new { item, tempItems })
                .SelectMany(t => t.tempItems.DefaultIfEmpty(), (t, temp) => new { t, temp })
                .Where(t => ReferenceEquals(null, t.temp) || t.temp.Equals(default(T)))
                .Select(t => t.t.item);
        }

        public static ILookup<TKey, T> ToLookup<T, TKey>(this IEnumerable<T> source, Func<T, TKey[]> keysSelector)
        {
            var dict = new Dictionary<TKey, List<T>>();

            foreach (var item in source)
            {
                foreach (var key in keysSelector(item))
                {
                    if (!dict.ContainsKey(key))
                    {
                        dict[key] = new List<T> { item };
                    }
                    else
                    {
                        dict[key].Add(item);
                    }
                }
            }

            return dict.SelectMany(p => p.Value.Select(x => new { p.Key, Value = x }))
                       .ToLookup(pair => pair.Key, pair => pair.Value);
        }
    }
}
