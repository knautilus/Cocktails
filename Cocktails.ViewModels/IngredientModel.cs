using System;

namespace Cocktails.ViewModels
{
    public class IngredientModel : BaseModel
    {
        public string Name { get; set; }

        public Guid FlavorId { get; set; }
        public FlavorModel Flavor { get; set; }

        public Guid CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}
