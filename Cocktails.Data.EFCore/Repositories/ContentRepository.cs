using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cocktails.Data.EFCore.Repositories
{
    public class ContentRepository<TKey, TEntity> : Repository<TEntity>, IContentRepository<TKey, TEntity>
        where TKey : struct
        where TEntity : BaseContentEntity<TKey>
    {
        public ContentRepository(DbContext context) : base(context) { }

        public override Task<TEntity> GetSingleAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken cancellationToken)
        {
            return query(Entities.AsQueryable()).FirstOrDefaultAsync(cancellationToken);
        }

        public override async Task<TEntity[]> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken cancellationToken)
        {
            var result = await query(Entities.AsQueryable()).ToArrayAsync(cancellationToken);
            return result;
        }

        public override async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken, bool autoCommit = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var entry = Entities.Add(entity);
            IgnoreReadonlyFields(entry);

            if (autoCommit)
            {
                await CommitAsync(cancellationToken);
            }

            return entity;
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool autoCommit = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity = SetModifiedDate(entity);
            var entry = Entities.Update(entity);
            IgnoreReadonlyFields(entry);

            if (autoCommit)
            {
                await CommitAsync(cancellationToken);
            }

            return entity;
        }

        public override async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool autoCommit = true)
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
