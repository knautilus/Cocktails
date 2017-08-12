using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Cocktails.Data.Domain;

namespace Cocktails.Data.EntityFramework.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext context;
        private DbSet<T> entities;

        public Repository(DbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public Task<IEnumerable<T>> GetAllAsync(CancellationToken token)
        {
            return Task.FromResult(entities.AsEnumerable());
        }

        public Task<T> GetAsync(Func<IQueryable<T>, IQueryable<T>> query, CancellationToken token)
        {
            return query(entities.AsQueryable()).FirstOrDefaultAsync(token);
        }

        public async Task<T> InsertAsync(T entity, CancellationToken token)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            await context.SaveChangesAsync(token);
            return entity;
        }

        public Task UpdateAsync(T entity, CancellationToken token)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            return context.SaveChangesAsync(token);
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
