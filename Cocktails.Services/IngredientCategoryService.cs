using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;

namespace Cocktails.Services
{
    public class IngredientCategoryService : BaseService<IngredientCategory>
    {
        public IngredientCategoryService(IRepository<IngredientCategory> repository) : base(repository)
        {
        }
    }
}
