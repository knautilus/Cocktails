using Cocktails.Entities.Sql;
using Cocktails.Models.Common;

namespace Cocktails.Models.Cms.Requests.CocktailCategories
{
    public class CocktailCategoryGetQueryableQuery : GetQueryableQuery<CocktailCategory>
    {
        public string Name { get; set; }
    }
}
