using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktails.Data.Domain
{
    public class Flavor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
