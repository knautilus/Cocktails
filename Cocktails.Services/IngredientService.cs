using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;

namespace Cocktails.Services
{
    public class IngredientService : BaseService<Ingredient>
    {
        public IngredientService(IRepository<Ingredient> repository) : base(repository) {}
    }
}
