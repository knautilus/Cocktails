using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Cocktails.Common.Exceptions;
using Cocktails.Common.Extensions;
using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;
using Cocktails.Mapper;
using Cocktails.ViewModels;

namespace Cocktails.Services
{
    public class IngredientService : BaseService<Ingredient, IngredientModel>
    {
        public IngredientService(IRepository<Ingredient> repository, IModelMapper mapper)
            : base(repository, mapper) {}

        public override async Task<IngredientModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetSingleAsync(x => x
                .Where(y => y.Id == id)
                .Include(y => y.Flavor)
                .Include(y => y.Category), cancellationToken);
            return _mapper.Map<IngredientModel>(result);
        }

        public override async Task<IEnumerable<IngredientModel>> GetAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(x => x
                .Include(y => y.Flavor)
                .Include(y => y.Category), cancellationToken);
            return _mapper.Map<IEnumerable<IngredientModel>>(result);
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
