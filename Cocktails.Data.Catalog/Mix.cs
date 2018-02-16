using System;

namespace Cocktails.Data.Catalog
{
    public class Mix : BaseContentEntity<Guid>
    {
        public Cocktail Cocktail { get; set; }

        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public decimal Amount { get; set; }
    }
}
