using Cocktails.Entities.Sql;
using Cocktails.Models.Common;

namespace Cocktails.Models.Cms.Requests.Ingredients
{
    public class IngredientGetQueryableQuery : GetQueryableQuery<Ingredient>
    {
        public string Name { get; set; }
    }
}
