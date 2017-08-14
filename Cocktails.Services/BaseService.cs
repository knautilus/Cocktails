using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;

namespace Cocktails.Services
{
    public abstract class BaseService<T> : IService<T>
        where T : BaseEntity, new()
    {
        protected readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async virtual Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetSingleAsync(x => x.Where(y => y.Id == id), cancellationToken);
            return result;
        }

        public async virtual Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(x => x, cancellationToken);
            return result;
        }

        public virtual Task<T> CreateAsync(T model, CancellationToken cancellationToken)
        {
            return _repository.InsertAsync(model, cancellationToken);
        }

        public virtual async Task<T> UpdateAsync(Guid id, T model, CancellationToken cancellationToken)
        {
            model.Id = id;
            try
            {
                return await _repository.UpdateAsync(model, cancellationToken);
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.DeleteAsync(new T { Id = id }, cancellationToken);
        }
    }
}
