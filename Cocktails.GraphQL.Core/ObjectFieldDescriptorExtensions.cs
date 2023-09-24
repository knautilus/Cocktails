using Cocktails.Data.Core;
using Cocktails.Entities.Common;
using Cocktails.Models.Common;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;

namespace Cocktails.GraphQL.Core
{
    public static class ObjectFieldDescriptorExtensions
    {
        public static IObjectFieldDescriptor UseCustomPaging<TKey, TEntity, TRequest>(this IObjectFieldDescriptor descriptor)
            where TKey : struct
            where TEntity : BaseEntity<TKey>
            where TRequest : IPagingQuery, IQuery
        {
            return descriptor
                .UsePaging(options: new PagingOptions { IncludeTotalCount = true, AllowBackwardPagination = false })
                .Use<CollectionMiddleware<TKey, TEntity, TRequest>>();
        }
    }
}
