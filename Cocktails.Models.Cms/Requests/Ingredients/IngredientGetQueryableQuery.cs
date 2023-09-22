using Cocktails.Models.Common;

namespace Cocktails.Models.Cms.Requests.Ingredients
{
    public class IngredientGetQueryableQuery : GetQueryableQuery
    {
        public string Name { get; set; }
    }
}
