using Cocktails.Data.Elasticsearch;
using Cocktails.Entities.Elasticsearch;
using Cocktails.Models.Site.Requests.Cocktails;
using Nest;
using System.Linq.Expressions;

namespace Cocktails.Cqrs.Nosql.QueryHandlers.Cocktails
{
    public class CocktailGetManyQueryHandler : GetManyQueryHandler<CocktailDocument, CocktailGetManyQuery, CocktailSort>
    {
        public CocktailGetManyQueryHandler(IElasticClient elasticClient, IIndexConfiguration indexConfiguration) : base(elasticClient, indexConfiguration)
        {
        }

        protected override Expression<Func<CocktailDocument, object>> GetSortSelector(CocktailSort sort)
        {
            switch (sort)
            {
                case CocktailSort.Id:
                    return x => x.Id;
                case CocktailSort.Name:
                    return x => x.Name.Suffix("keyword");
                default:
                    return null;
            }
        }
    }
}
