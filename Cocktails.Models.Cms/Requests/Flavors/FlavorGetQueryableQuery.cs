using Cocktails.Entities.Sql;
using Cocktails.Models.Common;

namespace Cocktails.Models.Cms.Requests.Flavors
{
    public class FlavorGetQueryableQuery : GetQueryableQuery<Flavor>
    {
        public string Name { get; set; }
    }
}
