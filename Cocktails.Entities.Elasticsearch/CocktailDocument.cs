using Cocktails.Entities.Common;

namespace Cocktails.Entities.Elasticsearch
{
    public class CocktailDocument : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MixDocument> Mixes { get; set; }
        public FlavorDocument Flavor { get; set; }
        public CocktailCategoryDocument CocktailCategory { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
