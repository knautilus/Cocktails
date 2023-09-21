using Cocktails.Data.EFCore.Extensions;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Ingredients;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.QueryHandlers.Ingredients
{
    public class IngredientGetQueryableQueryHandler : GetQueryableQueryHandler<IngredientGetQueryableQuery, Ingredient>
    {
        public IngredientGetQueryableQueryHandler(DbContext dbContext) : base(dbContext)
        {
        }

        protected override Func<IQueryable<Ingredient>, IngredientGetQueryableQuery, IQueryable<Ingredient>> Filter =>
            (source, request) => source
                .ConditionalWhere(!string.IsNullOrWhiteSpace(request.Name), x => EF.Functions.Like(x.Name, request.Name.ToLikePattern()));
    }
}
