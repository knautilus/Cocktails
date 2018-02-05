using System.Collections.Generic;

namespace Cocktails.Data.Domain
{
    public class Cocktail : BaseContentEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Mix> Mixes { get; set; }
    }
}
