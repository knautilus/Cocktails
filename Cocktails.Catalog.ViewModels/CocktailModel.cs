using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cocktails.Common.Models;

namespace Cocktails.Catalog.ViewModels
{
    public class CocktailModel : BaseModel<Guid>
    {
        [Required, MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public List<MixModel> Mixes { get; set; }
    }
}
