using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cocktails.Catalog.ViewModels
{
    public class IngredientModel : BaseModel
    {
        [Required, MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public Guid FlavorId { get; set; }
        [ReadOnly(true)]
        public FlavorModel Flavor { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
        [ReadOnly(true)]
        public CategoryModel Category { get; set; }
    }
}
