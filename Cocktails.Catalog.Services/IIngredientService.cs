using Cocktails.Catalog.ViewModels;
using Cocktails.Data.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cocktails.Catalog.Services
{
    public interface IIngredientService : IService<Ingredient, IngredientModel>
    {
        Task<CollectionWrapper<IngredientModel>> GetByCategoryIdAsync(Guid categoryId, QueryContext context, CancellationToken cancellationToken);
    }
}
