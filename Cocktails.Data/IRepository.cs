﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Cocktails.Data.Domain;

namespace Cocktails.Data
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetSingleAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken cancellationToken);
        Task<TEntity[]> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken cancellationToken);
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken, bool autoCommit = true);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool autoCommit = true);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool autoCommit = true);
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
