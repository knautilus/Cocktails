using Cocktails.Catalog.ViewModels;
using Cocktails.Data.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Cocktails.Catalog.Services
{
    public interface ICocktailService : IService<Cocktail,  CocktailModel>
    {
        Task<CollectionWrapper<CocktailModel>> GetByCocktailNameAsync(string cocktailName, QueryContext context, CancellationToken cancellationToken);
    }
}
