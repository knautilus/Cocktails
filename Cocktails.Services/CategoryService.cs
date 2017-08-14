using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;

namespace Cocktails.Services
{
    public class CategoryService : BaseService<Category>
    {
        public CategoryService(IRepository<Category> repository) : base(repository) {}
    }
}
