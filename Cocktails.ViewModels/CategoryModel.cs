using System.ComponentModel.DataAnnotations;

namespace Cocktails.ViewModels
{
    public class CategoryModel : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
