using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cocktails.Common.Models;
using Newtonsoft.Json;

namespace Cocktails.Catalog.ViewModels
{
    public class MixModel : BaseModel<Guid>
    {
        [JsonIgnore]
        public override Guid Id { get => base.Id; set => base.Id = value; }

        [Required]
        public Guid IngredientId { get; set; }
        [ReadOnly(true)]
        public IngredientModel Ingredient { get; set; }

        public decimal Amount { get; set; }
    }
}
