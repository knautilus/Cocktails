using System;
using System.Collections.Generic;

namespace Cocktails.Data.Domain
{
    public class Flavor : BaseContentEntity<Guid>
    {
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
