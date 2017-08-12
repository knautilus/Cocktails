using System;
using System.Collections.Generic;

namespace Cocktails.Data.Domain
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }

        public Guid FlavorId { get; set; }
        public Flavor Flavor { get; set; }

        public Guid IngredientCategoryId { get; set; }
        public IngredientCategory IngredientCategory { get; set; }

        public List<Mix> Mixes { get; set; }
    }
}
