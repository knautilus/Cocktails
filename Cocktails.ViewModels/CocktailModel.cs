using System.Collections.Generic;

namespace Cocktails.ViewModels
{
    public class CocktailModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<MixModel> Mixes { get; set; }
    }
}
