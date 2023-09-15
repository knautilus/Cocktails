﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace Cocktails.Catalog.ViewModels
{
    public class MixModel : BaseModel
    {
        [Required]
        public Guid IngredientId { get; set; }

        public IngredientModel Ingredient { get; set; }

        public decimal Amount { get; set; }
    }
}
