using Cocktails.Data.EFCore.Extensions;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.CocktailCategories;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.QueryHandlers.Categories
{
    public class CocktailCategoryGetQueryableQueryHandler : GetQueryableQueryHandler<CocktailCategoryGetQueryableQuery, CocktailCategory>
    {
        public CocktailCategoryGetQueryableQueryHandler(DbContext dbContext) : base(dbContext)
        {
        }

        protected override Func<IQueryable<CocktailCategory>, CocktailCategoryGetQueryableQuery, IQueryable<CocktailCategory>> Filter =>
            (source, request) => source
                .ConditionalWhere(!string.IsNullOrWhiteSpace(request.Name), x => EF.Functions.Like(x.Name, request.Name.ToLikePattern()));
    }
}
