using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;

namespace Cocktails.Services
{
    public class FlavorService : IService<Flavor>
    {
        private readonly IRepository<Flavor> _repository;

        public FlavorService(IRepository<Flavor> repository)
        {
            _repository = repository;
        }

        public async Task<Flavor> ReadAsync(Guid key, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(x => x.Where(y => y.Id == key), cancellationToken);
            if (result == null)
            {
                throw new Exception();
            }
            return result;
        }

        public Task<Flavor> CreateAsync(Flavor model, CancellationToken cancellationToken)
        {
            return _repository.InsertAsync(model, cancellationToken);
        }
    }
}
