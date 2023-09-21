using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Mixes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.QueryHandlers.Mixes
{
    public class MixesGetByCocktailIdsQueryHandler : IRequestHandler<MixesGetByCocktailIdsQuery, IQueryable<Mix>>
    {
        private readonly DbContext _dbContext;

        public MixesGetByCocktailIdsQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IQueryable<Mix>> Handle(MixesGetByCocktailIdsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Set<Mix>()
                .Where(x => request.CocktailIds.Contains(x.CocktailId)));
        }
    }
}
