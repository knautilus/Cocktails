using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Cocktails.Common.Exceptions;
using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;
using Cocktails.Mapper;
using Cocktails.ViewModels;
using Cocktails.Common;

namespace Cocktails.Services
{
    public abstract class BaseService<TEntity, TModel> : IService<TEntity, TModel>
        where TModel : BaseModel
        where TEntity : BaseEntity, new()
    {
        protected readonly IRepository<TEntity> Repository;
        protected readonly IModelMapper Mapper;

        protected virtual Func<IQueryable<TEntity>, IQueryable<TEntity>> IncludeFunction =>
            x => x;

        public BaseService(IRepository<TEntity> repository, IModelMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await Repository.GetSingleAsync(x => IncludeFunction(x.Where(y => y.Id == id)), cancellationToken);
            return Mapper.Map<TModel>(result);
        }

        public virtual async Task<CollectionWrapper<TModel>> GetAllAsync(QueryContext context, CancellationToken cancellationToken)
        {
            var result = await Repository.GetAsync(x => GetQuery(context)(IncludeFunction(x)), cancellationToken);
            return WrapCollection(Mapper.Map<IEnumerable<TModel>>(result), context);
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

        protected Func<IQueryable<TEntity>, IQueryable<TEntity>> GetQuery(QueryContext context)
        {
            Expression<Func<TEntity, DateTimeOffset>> sortSelector = x => x.ModifiedDate;
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> resultSortFunction;
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortFunction;
            Func<IQueryable<TEntity>, IQueryable<TEntity>> cursorFunction;
            if (context.IsSortAsc)
            {
                resultSortFunction = x => x.OrderBy(sortSelector);
                if (context.BeforeDate.HasValue)
                {
                    sortFunction = x => x.OrderByDescending(sortSelector);
                    cursorFunction = x => x.Where(Predicates.LessThanPredicate(sortSelector, () => context.BeforeDate.Value));
                }
                else if (context.AfterDate.HasValue)
                {
                    sortFunction = x => x.OrderBy(sortSelector);
                    cursorFunction = x => x.Where(Predicates.GreaterThanPredicate(sortSelector, () => context.AfterDate.Value));
                }
                else
                {
                    sortFunction = x => x.OrderBy(sortSelector);
                    cursorFunction = x => x;
                }
            }
            else
            {
                resultSortFunction = x => x.OrderByDescending(sortSelector);
                if (context.BeforeDate.HasValue)
                {
                    sortFunction = x => x.OrderBy(sortSelector);
                    cursorFunction = x => x.Where(Predicates.GreaterThanPredicate(sortSelector, () => context.BeforeDate.Value));
                }
                else if (context.AfterDate.HasValue)
                {
                    sortFunction = x => x.OrderByDescending(sortSelector);
                    cursorFunction = x => x.Where(Predicates.LessThanPredicate(sortSelector, () => context.AfterDate.Value));
                }
                else
                {
                    sortFunction = x => x.OrderByDescending(sortSelector);
                    cursorFunction = x => x;
                }
            }
            Func<IQueryable<TEntity>, IQueryable<TEntity>> query = x => sortFunction(cursorFunction(x)).Take(context.Count);
            if(context.BeforeDate.HasValue)
            {
                return x => resultSortFunction(query(x));
            }
            return query;
        }

        protected CollectionWrapper<TModel> WrapCollection(IEnumerable<TModel> data, QueryContext context)
        {
            Func<TModel, string> cursorSelector = x => x?.ModifiedDate.Ticks.ToString();
            CollectionWrapper<TModel> wrapper = new CollectionWrapper<TModel>
            {
                Data = data,
                Paging = new PagingModel()
            };
            if (data.Count() == context.Count)
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
