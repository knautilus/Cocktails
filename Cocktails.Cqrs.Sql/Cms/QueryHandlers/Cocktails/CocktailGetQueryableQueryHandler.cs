using Cocktails.Data.EFCore.Extensions;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Cocktails;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.QueryHandlers.Cocktails
{
    public class CocktailGetQueryableQueryHandler : GetQueryableQueryHandler<CocktailGetQueryableQuery, Cocktail>
    {
        public CocktailGetQueryableQueryHandler(DbContext dbContext) : base(dbContext)
        {
        }

        protected override Func<IQueryable<Cocktail>, CocktailGetQueryableQuery, IQueryable<Cocktail>> Filter =>
            (source, request) => source
                .ConditionalWhere(!string.IsNullOrWhiteSpace(request.Name),
                    x => EF.Functions.Like(x.Name, request.Name.ToLikePattern()))
                .ConditionalWhere(request.IngredientId.HasValue,
                    x => x.Mixes.Any(m => m.IngredientId == request.IngredientId.Value))
                .ConditionalWhere(request.CocktailCategoryId.HasValue,
                    x => x.CocktailCategoryId == request.CocktailCategoryId.Value)
                .ConditionalWhere(request.FlavorId.HasValue, x => x.CocktailCategoryId == request.FlavorId.Value);
    }
}
