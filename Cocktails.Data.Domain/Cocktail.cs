using System.Collections.Generic;

namespace Cocktails.Data.Entities
{
    public class Cocktail : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Mix> Mixes { get; set; }
    }
}
