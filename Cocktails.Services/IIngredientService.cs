using Cocktails.Data.Domain;
using Cocktails.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cocktails.Services
{
    public interface IIngredientService : IService<Ingredient, IngredientModel>
    {
        Task<IEnumerable<IngredientModel>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken);
    }
}
