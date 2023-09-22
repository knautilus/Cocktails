using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Data.Elasticsearch;
using Cocktails.Models.Common;
using Nest;
using System.Linq.Expressions;

namespace Cocktails.Cqrs.Nosql
{
    public class GetManyQueryHandler<TEntity, TGetManyQuery, TSortFieldEnum> : IQueryHandler<TGetManyQuery, TEntity[]>
        where TEntity : class
        where TGetManyQuery : GetManyQuery<TSortFieldEnum>
        where TSortFieldEnum : struct
    {
        protected readonly IElasticClient _elasticClient;
        protected readonly IIndexConfiguration _indexConfiguration;

        public GetManyQueryHandler(IElasticClient elasticClient, IIndexConfiguration indexConfiguration)
        {
            _elasticClient = elasticClient;
            _indexConfiguration = indexConfiguration;
        }

        public virtual async Task<TEntity[]> Handle(TGetManyQuery request, CancellationToken cancellationToken)
        {
            var queryContainer = GetQueryContainer(request);

            var descriptor = new SearchDescriptor<TEntity>()
                .Query(queryContainer)
                .Sort(GetSortSelector(request.SortField), request.SortDirection)
                .Paginate(request);

            var response = await _elasticClient.SearchAsync<TEntity>(
                descriptor.Index(_indexConfiguration.GetIndexName<TEntity>()),
                cancellationToken);

            if (!response.IsValid && !response.ApiCall.Success)
            {
                throw new Exception($"Elasticsearch error: {response.OriginalException.Message}");
            }

            var result = response.Hits.Select(x => x.Source).ToArray();

            return result;
        }

        protected virtual Func<QueryContainerDescriptor<TEntity>, QueryContainer> GetQueryContainer(TGetManyQuery request)
        {
            return x => x.Bool(b => b.Must(m => m.MatchAll()));
        }

        protected virtual Expression<Func<TEntity, object>> GetSortSelector(TSortFieldEnum sort)
        {
            return x => x;
        }
    }
}
