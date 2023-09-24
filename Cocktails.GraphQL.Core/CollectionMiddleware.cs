using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Data.Core;
using Cocktails.Entities.Common;
using Cocktails.Models.Common;
using HotChocolate.Resolvers;
using HotChocolate.Types.Pagination;

namespace Cocktails.GraphQL.Core
{
    public class CollectionMiddleware<TKey, TEntity, TRequest>
        where TEntity : BaseEntity<TKey> where TKey : struct
        where TRequest : IPagingQuery, IQuery
    {
        private readonly FieldDelegate _next;

        public CollectionMiddleware(
            FieldDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            await _next(context);

            var request = context.ArgumentValue<TRequest>("request");

            if (context.Result is TEntity[] data)
            {
                var edges = data.Select(x => new Edge<TEntity>(x, x.Id.ToString()))
                    .ToList();

                var totalCount = edges.Count;
                var pageInfo = new ConnectionPageInfo(false, false, null, null);

                if (context.Selection.SelectionSet.Selections.Any(x => x.ToString().Contains("totalCount") || x.ToString().Contains("pageInfo")))
                {
                    var queryProcessor = context.Service<IQueryProcessor>();

                    totalCount = await queryProcessor.Process<int>(request, new CancellationToken());

                    var hasPreviousPage = request.Offset > 0;
                    var hasNextPage = request.Offset + request.First < totalCount;

                    pageInfo = new ConnectionPageInfo(hasNextPage, hasPreviousPage, null, null);
                }

                context.Result = new Connection<TEntity>(edges, pageInfo, ct => ValueTask.FromResult(totalCount));
            }
        }
    }
}