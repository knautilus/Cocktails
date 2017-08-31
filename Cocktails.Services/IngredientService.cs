﻿using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Cocktails.Common.Exceptions;
using Cocktails.Common.Extensions;
using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;
using Cocktails.Mapper;
using Cocktails.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cocktails.Services
{
    public class IngredientService : BaseService<Ingredient, IngredientModel>, IIngredientService
    {
        protected override Func<IQueryable<Ingredient>, IQueryable<Ingredient>> IncludeFunction =>
            x => x
                .Include(y => y.Flavor)
                .Include(y => y.Category);

        public IngredientService(IRepository<Ingredient> repository, IModelMapper mapper)
            : base(repository, mapper) {}

        public async Task<CollectionWrapper<IngredientModel>> GetByCategoryIdAsync(Guid categoryId, QueryContext context, CancellationToken cancellationToken)
        {
            var result = await Repository.GetAsync(
                x => GetQuery(context)(
                    IncludeFunction(x.Where(y => y.CategoryId == categoryId))),
                cancellationToken);
            return WrapCollection(Mapper.Map<IEnumerable<IngredientModel>>(result), context);
        }

        protected override Exception GetDetailedException(Exception exception)
        {
            if (exception is DbUpdateConcurrencyException)
            {
                return new NotFoundException("Id not found");
            }
            if (exception is DbUpdateException dbEx)
            {
                if (dbEx.Contains("FK_Ingredients_Flavors_FlavorId"))
                {
                    return new BadRequestException("FlavorId not found");
                }
                if (dbEx.Contains("FK_Ingredients_Categories_CategoryId"))
                {
                    return new BadRequestException("CategoryId not found");
                }
                return new BadRequestException(dbEx.GetMostInner()?.Message);
            }
            return exception;
        }
    }
}
