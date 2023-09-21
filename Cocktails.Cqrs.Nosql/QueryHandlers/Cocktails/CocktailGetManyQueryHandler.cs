using Cocktails.Entities.Elasticsearch;
using Cocktails.Models.Common;
using Cocktails.Models.Site.Requests.Cocktails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cocktails.Data.Elasticsearch;
using Nest;

namespace Cocktails.Cqrs.Nosql.QueryHandlers.Cocktails
{
    public class CocktailGetManyQueryHandler : GetManyQueryHandler<CocktailDocument, GetManyQuery<CocktailDocument, CocktailSort>, CocktailSort>
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
