using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cocktails.ViewModels
{
    public class CocktailModel : BaseModel
    {
        [Required, MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public List<MixModel> Mixes { get; set; }
    }
}
