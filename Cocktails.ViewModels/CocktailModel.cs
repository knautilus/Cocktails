using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cocktails.ViewModels
{
    public class CocktailModel : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public List<MixModel> Mixes { get; set; }
    }
}
