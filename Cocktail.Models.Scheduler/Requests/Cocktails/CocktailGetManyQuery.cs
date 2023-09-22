using Cocktails.Models.Common;

namespace Cocktails.Models.Scheduler.Requests.Cocktails
{
    public class CocktailGetManyQuery : GetManyQuery<int>
    {
        public long[] CocktailIds { get; set; }
    }
}
