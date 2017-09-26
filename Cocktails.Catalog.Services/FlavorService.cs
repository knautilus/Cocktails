using Cocktails.Catalog.ViewModels;
using Cocktails.Data;
using Cocktails.Data.Domain;
using Cocktails.Mapper;

namespace Cocktails.Catalog.Services
{
    public class FlavorService : BaseService<Flavor, FlavorModel>
    {
        public FlavorService(IRepository<Flavor> repository, IModelMapper mapper)
            : base(repository, mapper) {}
    }
}
