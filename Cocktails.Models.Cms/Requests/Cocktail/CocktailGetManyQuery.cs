namespace Cocktails.Models.Cms.Requests.Cocktail
{
    public class CocktailGetManyQuery
    {
        public string Name { get; set; }
        public long? IngredientId { get; set; }
        public long? CocktailCategoryId { get; set; }
        public long? FlavorId { get; set; }
    }
}
