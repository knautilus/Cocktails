using Cocktails.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Cocktails.Responses
{
    public class FlavorModel : BaseModel<long>
    {
        [Required, MaxLength(128)]
        public string Name { get; set; }
    }
}
