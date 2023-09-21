using Cocktails.Entities.Sql;
using Cocktails.Models.Common;

namespace Cocktails.Models.Cms.Requests.Mixes
{
    public class MixesGetByCocktailIdsQuery : GetQueryableQuery<Mix>
    {
        public long[] CocktailIds { get; set; }
    }
}
