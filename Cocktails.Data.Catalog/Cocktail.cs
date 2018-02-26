﻿using System;
using System.Collections.Generic;

namespace Cocktails.Data.Catalog
{
    public class Cocktail : BaseContentEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Mix> Mixes { get; set; }
    }
}