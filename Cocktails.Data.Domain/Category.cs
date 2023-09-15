using System.Collections.Generic;

namespace Cocktails.Data.Entities
{
    public class Category : BaseEntity<long>
    {
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
