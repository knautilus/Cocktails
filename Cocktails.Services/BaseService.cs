using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;

namespace Cocktails.Services
{
    public class BaseService<T> : IService<T>
        where T : BaseEntity
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetSingleAsync(x => x.Where(y => y.Id == id), cancellationToken);
            if (result == null)
            {
                throw new Exception();
            }
            return result;
        }

        public Task<T> CreateAsync(T model, CancellationToken cancellationToken)
        {
            return _repository.InsertAsync(model, cancellationToken);
        }

        public Task<T> UpdateAsync(Guid id, T model, CancellationToken cancellationToken)
        {
            model.Id = id;
            return _repository.UpdateAsync(model, cancellationToken);
        }
    }
}
