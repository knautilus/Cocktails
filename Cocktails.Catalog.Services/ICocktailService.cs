using System;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Catalog.ViewModels;
using Cocktails.Common.Models;
using Cocktails.Common.Services;
using Cocktails.Data.Catalog;

namespace Cocktails.Catalog.Services
{
    public interface ICocktailService : IService<Guid, Cocktail, CocktailModel>
    {
        Task<CollectionWrapper<CocktailModel>> GetByCocktailNameAsync(string cocktailName, QueryContext context, CancellationToken cancellationToken);
    }
}
