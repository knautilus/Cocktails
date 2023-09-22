using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Entities.Sql;
using Cocktails.Models.Scheduler.Requests.Mixes;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Scheduler.QueryHandlers.Mixes
{
    public class MixesGetByCocktailIdsQueryHandler : IQueryHandler<MixesGetByCocktailIdsQuery, Mix[]>
    {
        private readonly DbContext _dbContext;

        public MixesGetByCocktailIdsQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Mix[]> Handle(MixesGetByCocktailIdsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<Mix>()
                .Where(x => request.CocktailIds.Contains(x.CocktailId))
                .ToArrayAsync(cancellationToken);
        }
    }
}
