using Cocktails.Entities.Common;
using System.Collections.Generic;

namespace Cocktails.Entities.Sql
{
    public class CocktailCategory : BaseContentEntity<long>
    {
        public string Name { get; set; }

        public List<Cocktail> Cocktails { get; set; }
    }
}
