using System.ComponentModel.DataAnnotations;

namespace Cocktails.ViewModels
{
    public class FlavorModel : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
