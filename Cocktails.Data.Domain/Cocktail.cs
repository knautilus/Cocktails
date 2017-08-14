using System.Collections.Generic;

namespace Cocktails.Data.Domain
{
    public class Cocktail : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Mix> Mixes { get; set; }
    }
}
