using System;
using Cocktails.Catalog.ViewModels;
using Cocktails.Common.Services;
using Cocktails.Data;
using Cocktails.Data.Catalog;
using Cocktails.Mapper;

namespace Cocktails.Catalog.Services.EFCore
{
    public class CategoryService : BaseService<Category, CategoryModel>
    {
        public CategoryService(IContentRepository<Guid, Category> repository, IModelMapper mapper)
            : base(repository, mapper) {}
    }
}
