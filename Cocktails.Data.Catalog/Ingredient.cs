using System;
using System.Collections.Generic;

namespace Cocktails.Data.Catalog
{
    public class Ingredient : BaseContentEntity<Guid>
    {
        public string Name { get; set; }

        public Guid FlavorId { get; set; }
        public Flavor Flavor { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Mix> Mixes { get; set; }
    }
}
