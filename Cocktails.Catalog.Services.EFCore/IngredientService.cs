using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Cocktails.Catalog.ViewModels;
using Cocktails.Common.Exceptions;
using Cocktails.Common.Extensions;
using Cocktails.Common.Models;
using Cocktails.Common.Services;
using Cocktails.Data;
using Cocktails.Data.Catalog;
using Cocktails.Mapper;

namespace Cocktails.Catalog.Services.EFCore
{
    public class IngredientService : BaseService<Ingredient, IngredientModel>, IIngredientService
    {
        protected override Func<IQueryable<Ingredient>, IQueryable<Ingredient>> IncludeFunction =>
            QueryFunctions.IngredientsIncludeFunction;

        public IngredientService(IContentRepository<Guid, Ingredient> repository, IModelMapper mapper)
            : base(repository, mapper) {}

        public async Task<CollectionWrapper<IngredientModel>> GetByCategoryIdAsync(Guid categoryId, QueryContext context, CancellationToken cancellationToken)
        {
            var result = await Repository.GetAsync(
                x => 
                    IncludeFunction(QueryFunctions.IngredientsByCategoryIdFunction(x, categoryId))
                    .Paginate(context, y => y.ModifiedDate),
                cancellationToken);
            return WrapCollection(Mapper.Map<IngredientModel[]>(result), context);
        }

        protected override Exception GetDetailedException(Exception exception)
        {
            if (exception is PrimaryKeyException)
            {
                return new NotFoundException("Id not found");
            }
            if (exception is ForeignKeyException dbEx)
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
