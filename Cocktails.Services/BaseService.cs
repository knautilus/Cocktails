using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Cocktails.Common.Exceptions;
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
        protected readonly IRepository<TEntity> Repository;
        protected readonly IModelMapper Mapper;
        protected virtual Func<IQueryable<TEntity>, IQueryable<TEntity>> IncludeQuery =>
            x => x;

        public BaseService(IRepository<TEntity> repository, IModelMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await Repository.GetSingleAsync(x => IncludeQuery(x.Where(y => y.Id == id)), cancellationToken);
            return Mapper.Map<TModel>(result);
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await Repository.GetAsync(x => IncludeQuery(x), cancellationToken);
            return Mapper.Map<IEnumerable<TModel>>(result);
        }

        public virtual async Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<TEntity>(model);
            try
            {
                var result = await Repository.InsertAsync(entity, cancellationToken);
                return await GetByIdAsync(result.Id, cancellationToken);
            }
            catch (Exception ex)
            {
                throw GetDetailedException(ex);
            }
        }

        public virtual async Task<TModel> UpdateAsync(Guid id, TModel model, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<TEntity>(model);
            entity.Id = id;
            try
            {
                var result = await Repository.UpdateAsync(entity, cancellationToken);
                return await GetByIdAsync(result.Id, cancellationToken);
            }
            catch (Exception ex)
            {
                throw GetDetailedException(ex);
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await Repository.DeleteAsync(new TEntity { Id = id }, cancellationToken);
            }
            catch (Exception ex)
            {
                throw GetDetailedException(ex);
            }
        }

        protected virtual Exception GetDetailedException(Exception exception)
        {
            if (exception is DbUpdateConcurrencyException)
            {
                return new NotFoundException("Id not found");
            }
            if (exception is DbUpdateException)
            {
                return new BadRequestException("Foreign Id not found");
            }
            return exception;
        }
    }
}
