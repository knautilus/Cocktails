using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Data.EFCore.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> Entities;

        public Repository(DbContext context)
        {
            Context = context;
            Entities = context.Set<TEntity>();
        }

        public virtual Task<TEntity> GetSingleAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken cancellationToken)
        {
            return query(Entities.AsQueryable()).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<TEntity[]> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken cancellationToken)
        {
            var result = await query(Entities.AsQueryable()).ToArrayAsync(cancellationToken);
            return result;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken, bool autoCommit = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Entities.Add(entity);

            if (autoCommit)
            {
                await CommitAsync(cancellationToken);
            }

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool autoCommit = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Entities.Update(entity);

            if (autoCommit)
            {
                await CommitAsync(cancellationToken);
            }

            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool autoCommit = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Entities.Remove(entity);

            if (autoCommit)
            {
                await CommitAsync(cancellationToken);
            }
        }

        public virtual async Task CommitAsync(CancellationToken cancellationToken)
        {
            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new PrimaryKeyException(ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ForeignKeyException(ex);
            }
        }
    }
}
