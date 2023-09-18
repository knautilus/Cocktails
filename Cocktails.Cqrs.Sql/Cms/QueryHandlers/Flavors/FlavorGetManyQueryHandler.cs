using Cocktails.Data.EFCore.Extensions;
using Cocktails.Data.Entities;
using Cocktails.Models.Cms.Requests.Flavors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.QueryHandlers
{
    public class FlavorGetManyQueryHandler : IRequestHandler<FlavorGetManyQuery, IQueryable<Flavor>>
    {
        private readonly DbContext _dbContext;

        public FlavorGetManyQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IQueryable<Flavor>> Handle(FlavorGetManyQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Set<Flavor>()
                .ConditionalWhere(!string.IsNullOrWhiteSpace(request.Name), x => EF.Functions.Like(x.Name, request.Name.ToLikePattern())));
        }
    }
}
