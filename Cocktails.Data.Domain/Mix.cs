namespace Cocktails.Data.Entities
{
    public class Mix
    {
        public long CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }

        public long IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public decimal Amount { get; set; }

        public long MeasureUnitId { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
    }
}
