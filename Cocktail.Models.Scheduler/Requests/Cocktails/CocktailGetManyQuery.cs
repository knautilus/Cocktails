using Cocktails.Entities.Sql;
using Cocktails.Models.Common;

namespace Cocktails.Models.Scheduler.Requests.Cocktails
{
    public class CocktailGetManyQuery : GetManyQuery<long, Cocktail[]>
    {
        public long[] CocktailIds { get; set; }
    }
}
