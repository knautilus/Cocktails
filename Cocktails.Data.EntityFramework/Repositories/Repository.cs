using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Cocktails.Data.Domain;
using Cocktails.Common.Exceptions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cocktails.Data.EntityFramework.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly DbContext _context;
        private DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
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
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity = UpdateModel(entity);
            var entry = _entities.Update(entity);
            UpdateEntry(entry);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException)
            {
                throw new ItemNotFoundException { Id = entity.Id };
            }
            return entity;
        }

        private T UpdateModel (T model)
        {
            model.ModifiedDate = DateTimeOffset.UtcNow;
            return model;
        }

        private EntityEntry<T> UpdateEntry(EntityEntry<T> entry)
        {
            entry.Property(x => x.CreatedDate).IsModified = false;
            return entry;
        }

        //public void Delete(T entity)
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("entity");
        //    }
        //    entities.Remove(entity);
        //    context.SaveChanges();
        //}
    }
}
