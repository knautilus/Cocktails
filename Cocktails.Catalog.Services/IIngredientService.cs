using System;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Catalog.ViewModels;
using Cocktails.Common.Models;
using Cocktails.Common.Services;
using Cocktails.Data.Catalog;

namespace Cocktails.Catalog.Services
{
    public interface IIngredientService : IService<Ingredient, IngredientModel>
    {
        Task<CollectionWrapper<IngredientModel>> GetByCategoryIdAsync(Guid categoryId, QueryContext context, CancellationToken cancellationToken);
    }
}
