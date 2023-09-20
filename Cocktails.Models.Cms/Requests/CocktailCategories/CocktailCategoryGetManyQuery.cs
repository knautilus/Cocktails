﻿using Cocktails.Entities.Sql;
using MediatR;

namespace Cocktails.Models.Cms.Requests.CocktailCategories
{
    public class CocktailCategoryGetManyQuery : IRequest<IQueryable<CocktailCategory>>
    {
        public string Name { get; set; }
    }
}
