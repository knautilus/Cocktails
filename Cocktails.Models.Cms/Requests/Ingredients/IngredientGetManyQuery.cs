﻿using Cocktails.Data.Entities;
using MediatR;

namespace Cocktails.Models.Cms.Requests.Ingredients
{
    public class IngredientGetManyQuery : IRequest<IQueryable<Ingredient>>
    {
        public string Name { get; set; }
    }
}