using Cocktails.Data.EFCore.Extensions;
using Cocktails.Entities.Sql;
using Cocktails.Models.Scheduler.Requests.Cocktails;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Scheduler.QueryHandlers.Cocktails
{
    public class CocktailGetManyQueryHandler : GetManyQueryHandler<Cocktail, CocktailGetManyQuery, int>
    {
        public CocktailGetManyQueryHandler(DbContext readDbContext) : base(readDbContext)
        {
        }

        protected override Func<IQueryable<Cocktail>, CocktailGetManyQuery, IQueryable<Cocktail>> Filter =>
            (source, request) => source
                .ConditionalWhere(request.CocktailIds != null && request.CocktailIds.Any(),
                    x => request.CocktailIds.Contains(x.Id));
    }
}
