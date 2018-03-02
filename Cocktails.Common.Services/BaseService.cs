using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Common.Exceptions;
using Cocktails.Common.Models;
using Cocktails.Data;
using Cocktails.Mapper;

namespace Cocktails.Common.Services
{
    public abstract class BaseService<TKey, TEntity, TModel> : IService<TKey, TEntity, TModel>
        where TKey : struct
        where TModel : BaseModel<TKey>
        where TEntity : BaseContentEntity<TKey>, new()
    {
        protected readonly IContentRepository<TKey, TEntity> Repository;
        protected readonly IModelMapper Mapper;

        protected virtual Func<IQueryable<TEntity>, IQueryable<TEntity>> IncludeFunction =>
            x => x;

        protected BaseService(IContentRepository<TKey, TEntity> repository, IModelMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<TModel> GetByIdAsync(TKey id, CancellationToken cancellationToken)
        {
            var result = await Repository.GetSingleAsync(x => IncludeFunction(x.WhereByKey(y => y.Id, id)), cancellationToken);
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

        public virtual async Task<TModel> UpdateAsync(TKey id, TModel model, CancellationToken cancellationToken)
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

        public virtual async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
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
            if (exception is PrimaryKeyException)
            {
                return new NotFoundException("Id not found");
            }
            if (exception is ForeignKeyException)
            {
                return new BadRequestException("Foreign Id not found");
            }
            return exception;
        }
    }
}
