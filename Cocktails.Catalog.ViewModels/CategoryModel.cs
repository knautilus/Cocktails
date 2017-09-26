using System.ComponentModel.DataAnnotations;

namespace Cocktails.Catalog.ViewModels
{
    public class CategoryModel : BaseModel
    {
        [Required, MaxLength(128)]
        public string Name { get; set; }
    }
}
