using Cocktails.Entities.Common;

namespace Cocktails.Entities.Elasticsearch
{
    public class CocktailCategoryDocument : BaseEntity<long>
    {
        public string Name { get; set; }
    }
}
