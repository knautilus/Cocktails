using System.Collections.Generic;

namespace Cocktails.Data.Domain
{
    public class IngredientCategory : BaseEntity
    {
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
