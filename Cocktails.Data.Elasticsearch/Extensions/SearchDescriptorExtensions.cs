using Cocktails.Data.Core;
using Nest;
using System.Linq.Expressions;

namespace Cocktails.Data.Elasticsearch.Extensions
{
    public static class SearchDescriptorExtensions
    {
        public static SearchDescriptor<T> Sort<T>(this SearchDescriptor<T> source,
            Expression<Func<T, object>> sortSelector, SortDirection sortDirection)
            where T : class
        {
            if (sortSelector == null)
            {
                return source;
            }

            if (sortDirection == SortDirection.Asc)
            {
                return source.Sort(x => x.Ascending(sortSelector));
            }

            return source.Sort(x => x.Descending(sortSelector));
        }

        public static SearchDescriptor<T> Paginate<T>(this SearchDescriptor<T> source,
            IPagingQuery context)
            where T : class
        {
            return source.Skip(context.Offset).Size(context.First);
        }

        public static QueryContainer GetByIds<T>(this QueryContainerDescriptor<T> source,
            long[] ids)
            where T : class
        {
            return ids.Any() ? source.Ids(y => y.Values(ids)) : source.MatchNone();
        }

        public static QueryContainer GetByIds<T>(this QueryContainerDescriptor<T> source,
            int[] ids)
            where T : class
        {
            return ids.Any() ? source.Ids(y => y.Values(ids.Select(id => (long)id).ToArray())) : source.MatchNone();
        }

        public static BoolQueryDescriptor<T> FilterOrSelectAll<T>(this BoolQueryDescriptor<T> source, IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> filters)
            where T : class
        {
            var must = filters as Func<QueryContainerDescriptor<T>, QueryContainer>[] ?? filters.ToArray();
            if (!must.Any())
            {
                return source.Must(x => x.MatchAll());
            }

            return source.Must(must);
        }
    }
}
