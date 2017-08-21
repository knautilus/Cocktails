using System;

using Newtonsoft.Json;

namespace Cocktails.ViewModels
{
    public class MixModel : BaseModel
    {
        [JsonIgnore]
        public override Guid Id { get => base.Id; set => base.Id = value; }

        public Guid IngredientId { get; set; }
        public IngredientModel Ingredient { get; set; }

        public decimal Amount { get; set; }
    }
}
