using Cocktails.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cocktails.Responses
{
    public class IngredientModel : BaseModel<long>
    {
        [Required, MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public Guid FlavorId { get; set; }

        public FlavorModel Flavor { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public CategoryModel Category { get; set; }
    }
}
