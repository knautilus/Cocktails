using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Options;

namespace Cocktails.Data.EntityFramework.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _entities;
        private readonly IRepositoryOptions _options;

        public Repository(DbContext context, IRepositoryOptions options)
        {
            _context = context;
            _entities = context.Set<T>();
            _options = options;
        }

        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_entities.AsEnumerable());
        }

        public Task<T> GetSingleAsync(Func<IQueryable<T>, IQueryable<T>> query, CancellationToken cancellationToken)
        {
            return query(_entities.AsQueryable()).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> query, CancellationToken cancellationToken)
        {
            var result = await query(_entities.AsQueryable()).ToListAsync();
            return result;
        }

        public async Task<T> InsertAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var entry = _entities.Add(entity);
            UpdateEntry(entry);

            if (_options.AutoCommit)
            {
                await CommitAsync(cancellationToken);
            }

            return entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity = SetModifiedDate(entity);
            var entry = _entities.Update(entity);
            UpdateEntry(entry);

            if (_options.AutoCommit)
            {
                await CommitAsync(cancellationToken);
            }

            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
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

        private T SetModifiedDate(T model)
        {
            model.ModifiedDate = DateTimeOffset.UtcNow;
            return model;
        }

        private EntityEntry<T> UpdateEntry(EntityEntry<T> entry)
        {
            entry.Property(x => x.CreatedDate).IsModified = false;
            return entry;
        }
    }
}
