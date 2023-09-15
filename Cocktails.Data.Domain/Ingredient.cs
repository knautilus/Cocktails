using System;
using System.Collections.Generic;

namespace Cocktails.Data.Entities
{
    public class Ingredient : BaseEntity<long>
    {
        public string Name { get; set; }

        public long FlavorId { get; set; }
        public Flavor Flavor { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Mix> Mixes { get; set; }
    }
}
