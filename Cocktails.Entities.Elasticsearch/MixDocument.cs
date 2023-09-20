namespace Cocktails.Entities.Elasticsearch
{
    public class MixDocument
    {
        public IngredientDocument IngredientDocument { get; set; }
        public decimal Amount { get; set; }
        public MeasureUnitDocument MeasureUnit { get; set; }
    }
}
