using Cocktails.Entities.Sql;
using Cocktails.Models.Common;

namespace Cocktails.Models.Cms.Requests.MeasureUnits
{
    public class MeasureUnitGetQueryableQuery : GetQueryableQuery<MeasureUnit>
    {
        public string Name { get; set; }
    }
}
