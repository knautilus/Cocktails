using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Mixes;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.QueryHandlers.Mixes
{
    public class MixesGetByCocktailIdsQueryHandler : GetQueryableQueryHandler<MixesGetByCocktailIdsQuery, Mix>
    {
        public MixesGetByCocktailIdsQueryHandler(DbContext dbContext) : base(dbContext)
        {
        }

        protected override Func<IQueryable<Mix>, MixesGetByCocktailIdsQuery, IQueryable<Mix>> Filter =>
            (source, request) => source
                .Where(x => request.CocktailIds.Contains(x.CocktailId));
    }
}
