using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;

namespace Cocktails.Services
{
    public class FlavorService : BaseService<Flavor>
    {
        public FlavorService(IRepository<Flavor> repository) : base(repository)
        {
        }
    }
}
