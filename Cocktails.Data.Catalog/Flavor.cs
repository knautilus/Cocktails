using System;
using System.Collections.Generic;

namespace Cocktails.Data.Catalog
{
    public class Flavor : BaseContentEntity<Guid>
    {
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
