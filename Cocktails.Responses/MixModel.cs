using Cocktails.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Cocktails.Responses
{
    public class MixModel : BaseModel<long>
    {
        [Required]
        public Guid IngredientId { get; set; }

        public IngredientModel Ingredient { get; set; }

        public decimal Amount { get; set; }
    }
}
