using Cocktails.Data.Elasticsearch;
using Cocktails.Entities.Elasticsearch;
using Cocktails.Models.Site.Requests.Cocktails;
using Nest;

namespace Cocktails.Cqrs.Nosql.QueryHandlers.Cocktails
{
    public class CocktailGetCountQueryHandler : GetCountQueryHandler<CocktailDocument, CocktailGetManyQuery, CocktailSort>
    {
        public CocktailGetCountQueryHandler(IElasticClient elasticClient, IIndexConfiguration indexConfiguration) : base(elasticClient, indexConfiguration)
        {
        }
    }
}
