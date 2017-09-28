using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Cocktails.Catalog.ViewModels;
using Cocktails.Common.Exceptions;
using Cocktails.Data;
using Cocktails.Data.Domain;
using Cocktails.Mapper;

namespace Cocktails.Catalog.Services.EFCore
{
    public abstract class BaseService<TEntity, TModel> : IService<TEntity, TModel>
        where TModel : BaseModel
        where TEntity : BaseEntity, new()
    {
        protected readonly IRepository<TEntity> Repository;
        protected readonly IModelMapper Mapper;

        protected virtual Func<IQueryable<TEntity>, IQueryable<TEntity>> IncludeFunction =>
            x => x;

        protected BaseService(IRepository<TEntity> repository, IModelMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await Repository.GetSingleAsync(x => IncludeFunction(QueryFunctions.GetByIdFunction<TEntity>()(x, id)), cancellationToken);
            return Mapper.Map<TModel>(result);
        }

        public virtual async Task<CollectionWrapper<TModel>> GetAllAsync(QueryContext context, CancellationToken cancellationToken)
        {
            var result = await Repository.GetAsync(x => IncludeFunction(x).Paginate(context, y => y.ModifiedDate), cancellationToken);
            return WrapCollection(Mapper.Map<TModel[]>(result), context);
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

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
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

        protected CollectionWrapper<TModel> WrapCollection(TModel[] data, QueryContext context)
        {
            Func<TModel, string> cursorSelector = x => x?.ModifiedDate.Ticks.ToString();
            var wrapper = new CollectionWrapper<TModel>
            {
                Data = data,
                Paging = new PagingModel()
            };
            if (data.Length == context.Limit)
            {
                wrapper.Paging.Before = cursorSelector(data.First());
                wrapper.Paging.After = cursorSelector(data.Last());
            }
            else
            {
                if (context.BeforeDate.HasValue)
                {
                    wrapper.Paging.Before = null;
                    wrapper.Paging.After = cursorSelector(data.LastOrDefault());
                }
                else
                {
                    wrapper.Paging.Before = cursorSelector(data.FirstOrDefault());
                    wrapper.Paging.After = null;
                }
            }
            return wrapper;
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
