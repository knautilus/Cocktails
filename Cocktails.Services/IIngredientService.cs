using Cocktails.Data.Domain;
using Cocktails.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cocktails.Services
{
    public interface IIngredientService : IService<Ingredient, IngredientModel>
    {
        Task<CollectionWrapper<IngredientModel>> GetByCategoryIdAsync(Guid categoryId, QueryContext context, CancellationToken cancellationToken);
    }
}
