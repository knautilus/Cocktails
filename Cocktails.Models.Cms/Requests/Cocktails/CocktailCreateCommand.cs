using Cocktails.Models.Common;

namespace Cocktails.Models.Cms.Requests.Cocktails
{
    public class CocktailCreateCommand : CreateCommand<long>, ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long FlavorId { get; set; }
        public long CocktailCategoryId { get; set; }
        public List<MixInput> Mixes { get; set; }
    }
}
