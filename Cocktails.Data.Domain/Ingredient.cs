using System;
using System.Collections.Generic;

namespace Cocktails.Data.Entities
{
    public class Ingredient : BaseEntity<long>
    {
        public string Name { get; set; }

        public List<Mix> Mixes { get; set; }
    }
}
