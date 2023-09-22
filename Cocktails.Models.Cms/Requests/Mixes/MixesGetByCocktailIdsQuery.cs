using Cocktails.Models.Common;

namespace Cocktails.Models.Cms.Requests.Mixes
{
    public class MixesGetByCocktailIdsQuery : GetQueryableQuery
    {
        public long[] CocktailIds { get; set; }
    }
}
