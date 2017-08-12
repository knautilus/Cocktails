using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktails.Data.Domain
{
    public class Mix
    {
        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }

        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public decimal Amount { get; set; }
    }
}
