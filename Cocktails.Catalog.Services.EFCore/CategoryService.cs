﻿using Cocktails.Catalog.ViewModels;
using Cocktails.Data;
using Cocktails.Data.Domain;
using Cocktails.Mapper;

namespace Cocktails.Catalog.Services.EFCore
{
    public class CategoryService : BaseService<Category, CategoryModel>
    {
        public CategoryService(IRepository<Category> repository, IModelMapper mapper)
            : base(repository, mapper) {}
    }
}
