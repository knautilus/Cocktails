using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;
using Cocktails.Mapper;
using Cocktails.ViewModels;

namespace Cocktails.Services
{
    public abstract class BaseService<TEntity, TModel> : IService<TEntity, TModel>
        where TModel : BaseModel
        where TEntity : BaseEntity, new()
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IModelMapper _mapper;

        public BaseService(IRepository<TEntity> repository, IModelMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async virtual Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetSingleAsync(x => x.Where(y => y.Id == id), cancellationToken);
            return _mapper.Map<TModel>(result);
        }

        public async virtual Task<IEnumerable<TModel>> GetAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(x => x, cancellationToken);
            return _mapper.Map<IEnumerable<TModel>>(result);
        }

        public virtual async Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(model);
            var result = await _repository.InsertAsync(entity, cancellationToken);
            return await GetByIdAsync(result.Id, cancellationToken);
        }

        public virtual async Task<TModel> UpdateAsync(Guid id, TModel model, CancellationToken cancellationToken)
        {
            model.Id = id;

            var entity = _mapper.Map<TEntity>(model);

            try
            {
                var result = await _repository.UpdateAsync(entity, cancellationToken);
                return await GetByIdAsync(result.Id, cancellationToken);
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.DeleteAsync(new TEntity { Id = id }, cancellationToken);
        }
    }
}
