﻿using Cocktails.Entities.Common;
using System.Collections.Generic;

namespace Cocktails.Entities.Sql
{
    public class Cocktail : BaseContentEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public long FlavorId { get; set; }
        public Flavor Flavor { get; set; }

        public long CocktailCategoryId { get; set; }
        public CocktailCategory CocktailCategory { get; set; }

        public List<Mix> Mixes { get; set; }
    }
}