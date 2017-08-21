using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;
using Cocktails.Mapper;
using Cocktails.ViewModels;

namespace Cocktails.Services
{
    public class FlavorService : BaseService<Flavor, FlavorModel>
    {
        public FlavorService(IRepository<Flavor> repository, IModelMapper mapper)
            : base(repository, mapper) {}
    }
}
