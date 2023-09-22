using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Data.Elasticsearch;
using Cocktails.Entities.Common;
using Cocktails.Models.Common;
using Nest;

namespace Cocktails.Cqrs.Nosql
{
    public class GetByIdQueryHandler<TKey, TEntity> : IQueryHandler<GetByIdQuery<TKey>, TEntity>
        where TEntity : BaseEntity<TKey>
    {
        protected readonly IElasticClient ElasticClient;
        protected readonly IIndexConfiguration IndexConfiguration;

        public GetByIdQueryHandler(IElasticClient elasticClient, IIndexConfiguration indexConfiguration)
        {
            ElasticClient = elasticClient;
            IndexConfiguration = indexConfiguration;
        }

        public async Task<TEntity> Handle(GetByIdQuery<TKey> request, CancellationToken cancellationToken)
        {
            var resp = await ElasticClient.GetAsync<TEntity>(
                new GetRequest(IndexConfiguration.GetIndexName<TEntity>(), Convert.ToInt64(request.Id)),
                cancellationToken);

            return resp.Source;
        }
    }
}
