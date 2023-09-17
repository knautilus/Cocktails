namespace Cocktails.Models.Cms.Requests.Cocktail
{
    public class MixInput
    {
        public long IngredientId { get; set; }
        public decimal Amount { get; set; }
        public long MeasureUnitId { get; set; }
    }
}
