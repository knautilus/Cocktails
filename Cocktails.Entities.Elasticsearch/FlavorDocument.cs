using Cocktails.Entities.Common;

namespace Cocktails.Entities.Elasticsearch
{
    public class FlavorDocument : BaseEntity<long>
    {
        public string Name { get; set; }
    }
}
