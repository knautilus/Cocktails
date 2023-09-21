using Cocktails.Cqrs.Nosql.Constants;
using Cocktails.Data.Elasticsearch;
using Cocktails.Entities.Common;
using Cocktails.Models.Common;
using MediatR;
using Nest;

namespace Cocktails.Cqrs.Nosql
{
    public class GetByIdsQueryHandler<TKey, TEntity> : IRequestHandler<GetByIdsQuery<TKey, TEntity>, TEntity[]>
        where TEntity : BaseEntity<TKey>
    {
        protected readonly IElasticClient _elasticClient;
        protected readonly IIndexConfiguration _indexConfiguration;

        public GetByIdsQueryHandler(IElasticClient elasticClient, IIndexConfiguration indexConfiguration)
        {
            _elasticClient = elasticClient;
            _indexConfiguration = indexConfiguration;
        }

        public async Task<TEntity[]> Handle(GetByIdsQuery<TKey, TEntity> request, CancellationToken cancellationToken)
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
