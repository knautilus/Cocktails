using System;
using System.Collections.Generic;

namespace Cocktails.Data.Domain
{
    public class Ingredient : BaseContentEntity
    {
        public string Name { get; set; }

        public Guid FlavorId { get; set; }
        public Flavor Flavor { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Mix> Mixes { get; set; }
    }
}
