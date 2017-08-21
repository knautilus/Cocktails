using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;
using Cocktails.Mapper;
using Cocktails.ViewModels;

namespace Cocktails.Services
{
    public class CategoryService : BaseService<Category, CategoryModel>
    {
        public CategoryService(IRepository<Category> repository, IModelMapper mapper)
            : base(repository, mapper) {}
    }
}
