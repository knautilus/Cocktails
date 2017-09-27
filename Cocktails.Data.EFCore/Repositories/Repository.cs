using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Cocktails.Data.Domain;

namespace Cocktails.Data.EFCore.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entities;
        private readonly IRepositoryOptions _options;

        public Repository(DbContext context, IRepositoryOptions options)
        {
            _context = context;
            _entities = context.Set<TEntity>();
            _options = options;
        }

        public Task<TEntity> GetSingleAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken cancellationToken)
        {
            return query(_entities.AsQueryable()).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity[]> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken cancellationToken)
        {
            var result = await query(_entities.AsQueryable()).ToArrayAsync(cancellationToken);
            return result;
        }

        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var entry = _entities.Add(entity);
            IgnoreReadonlyFields(entry);

            if (_options.AutoCommit)
            {
                await CommitAsync(cancellationToken);
            }

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity = SetModifiedDate(entity);
            var entry = _entities.Update(entity);
            IgnoreReadonlyFields(entry);

            if (_options.AutoCommit)
            {
                await CommitAsync(cancellationToken);
            }

            return entity;
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Remove(entity);

            if (_options.AutoCommit)
            {
                await CommitAsync(cancellationToken);
            }
        }

        public Task CommitAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        private TEntity SetModifiedDate(TEntity model)
        {
            model.ModifiedDate = DateTimeOffset.UtcNow;
            return model;
        }

        private void IgnoreReadonlyFields(EntityEntry<TEntity> entry)
        {
            entry.Property(x => x.CreatedDate).IsModified = false;
        }
    }
}
