using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;

namespace Cocktails.Services
{
    public class CocktailService : BaseService<Cocktail>
    {
        public CocktailService(IRepository<Cocktail> repository) : base(repository)
        {
        }
    }
}
