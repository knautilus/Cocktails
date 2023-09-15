using Cocktails.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Cocktails.Responses
{
    public class CocktailModel : BaseModel<long>
    {
        [Required, MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public List<MixModel> Mixes { get; set; }
    }
}
