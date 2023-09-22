using Cocktails.Models.Common;

namespace Cocktails.Models.Scheduler.Requests.Mixes
{
    public class MixesGetByCocktailIdsQuery : IQuery
    {
        public long[] CocktailIds { get; set; }
    }
}
