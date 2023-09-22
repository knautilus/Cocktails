using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Cqrs.Nosql.Constants;
using Cocktails.Data.Elasticsearch;
using Cocktails.Entities.Common;
using Cocktails.Models.Common;
using Nest;

namespace Cocktails.Cqrs.Nosql
{
    public class GetByIdsQueryHandler<TKey, TEntity> : IQueryHandler<GetByIdsQuery<TKey>, TEntity[]>
        where TEntity : BaseEntity<TKey>
    {
        protected readonly IElasticClient _elasticClient;
        protected readonly IIndexConfiguration _indexConfiguration;

        public GetByIdsQueryHandler(IElasticClient elasticClient, IIndexConfiguration indexConfiguration)
        {
            _elasticClient = elasticClient;
            _indexConfiguration = indexConfiguration;
        }

        public async Task<TEntity[]> Handle(GetByIdsQuery<TKey> request, CancellationToken cancellationToken)
        {
            Func<QueryContainerDescriptor<TEntity>, QueryContainer> queryContainer = x => x.GetByIds(request.Ids.Select(id => Convert.ToInt64(id)).ToArray());

            var descriptor = new SearchDescriptor<TEntity>()
                .Query(queryContainer)
                .Size(NosqlQueryParameters.MaxSize);

            var resp = await _elasticClient.SearchAsync<TEntity>(
                descriptor.Index(_indexConfiguration.GetIndexName<TEntity>()),
                cancellationToken);

            return resp.Hits.Select(x => x.Source).ToArray();
        }
    }
}
