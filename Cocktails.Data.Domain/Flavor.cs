﻿using System.Collections.Generic;

namespace Cocktails.Data.Entities
{
    public class Flavor : BaseEntity<long>
    {
        public string Name { get; set; }

        public List<Cocktail> Cocktails { get; set; }
    }
}