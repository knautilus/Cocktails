using Cocktails.Models.Common;

namespace Cocktails.Models.Cms.Requests.Cocktails
{
    public class CocktailGetQueryableQuery : GetQueryableQuery
    {
        public string Name { get; set; }
        public long? IngredientId { get; set; }
        public long? CocktailCategoryId { get; set; }
        public long? FlavorId { get; set; }
    }
}
