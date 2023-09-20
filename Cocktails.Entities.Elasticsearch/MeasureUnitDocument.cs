using Cocktails.Entities.Common;

namespace Cocktails.Entities.Elasticsearch
{
    public class MeasureUnitDocument : BaseEntity<long>
    {
        public string Name { get; set; }
    }
}
