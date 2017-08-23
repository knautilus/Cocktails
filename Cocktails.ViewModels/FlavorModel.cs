using System.ComponentModel.DataAnnotations;

namespace Cocktails.ViewModels
{
    public class FlavorModel : BaseModel
    {
        [Required, MaxLength(128)]
        public string Name { get; set; }
    }
}
