using Cocktails.Entities.Common;

namespace Cocktails.Entities.Elasticsearch
{
    public class IngredientDocument : BaseEntity<long>
    {
        public string Name { get; set; }
    }
}
