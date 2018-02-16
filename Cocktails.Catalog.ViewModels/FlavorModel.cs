using System.ComponentModel.DataAnnotations;
using Cocktails.Common.Models;

namespace Cocktails.Catalog.ViewModels
{
    public class FlavorModel : BaseModel
    {
        [Required, MaxLength(128)]
        public string Name { get; set; }
    }
}
