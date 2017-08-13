﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cocktails.Services
{
    public interface IService<TModel>
        where TModel : class
    {
        Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken);
        Task<TModel> UpdateAsync(Guid id, TModel model, CancellationToken cancellationToken);
    }
}
