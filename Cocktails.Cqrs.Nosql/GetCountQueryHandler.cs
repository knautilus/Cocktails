using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Data.Elasticsearch;
using Cocktails.Models.Common;
using Nest;

namespace Cocktails.Cqrs.Nosql
{
    public class GetCountQueryHandler<TEntity, TGetManyQuery, TSortFieldEnum> : IQueryHandler<TGetManyQuery, int>
        where TEntity : class
        where TGetManyQuery : GetManyQuery<TSortFieldEnum>
        where TSortFieldEnum : struct
    {
        protected readonly IElasticClient _elasticClient;
        protected readonly IIndexConfiguration _indexConfiguration;

        public GetCountQueryHandler(IElasticClient elasticClient, IIndexConfiguration indexConfiguration)
        {
            _elasticClient = elasticClient;
            _indexConfiguration = indexConfiguration;
        }

        public virtual async Task<int> Handle(TGetManyQuery request, CancellationToken cancellationToken)
        {
            var queryContainer = GetQueryContainer(request);

            var descriptor = new CountDescriptor<TEntity>()
                .Query(queryContainer);

            var response = await _elasticClient.CountAsync<TEntity>(
                x => descriptor.Index(_indexConfiguration.GetIndexName<TEntity>()),
                cancellationToken);

            if (!response.IsValid && !response.ApiCall.Success)
            {
                throw new Exception($"Elasticsearch error: {response.OriginalException.Message}");
            }

            return (int)response.Count;
        }

        protected virtual Func<QueryContainerDescriptor<TEntity>, QueryContainer> GetQueryContainer(TGetManyQuery request)
        {
            return x => x.Bool(b => b.Must(m => m.MatchAll()));
        }
    }
}
