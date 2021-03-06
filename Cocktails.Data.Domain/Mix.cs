﻿using System;

namespace Cocktails.Data.Domain
{
    public class Mix : BaseEntity
    {
        public Cocktail Cocktail { get; set; }

        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public decimal Amount { get; set; }
    }
}
