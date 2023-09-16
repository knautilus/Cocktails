using System.Collections.Generic;

namespace Cocktails.Data.Entities
{
    public class CocktailCategory : BaseEntity<long>
    {
        public string Name { get; set; }

        public List<Cocktail> Cocktails { get; set; }
    }
}
