using System.Collections.Generic;

namespace Cocktails.Data.Domain
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
