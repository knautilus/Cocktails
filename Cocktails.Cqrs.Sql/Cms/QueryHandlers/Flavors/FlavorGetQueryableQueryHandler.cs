using Cocktails.Data.EFCore.Extensions;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Flavors;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.QueryHandlers.Flavors
{
    public class FlavorGetQueryableQueryHandler : GetQueryableQueryHandler<FlavorGetQueryableQuery, Flavor>
    {
        public FlavorGetQueryableQueryHandler(DbContext dbContext) : base(dbContext)
        {
        }

        protected override Func<IQueryable<Flavor>, FlavorGetQueryableQuery, IQueryable<Flavor>> Filter =>
            (source, request) => source
                .ConditionalWhere(!string.IsNullOrWhiteSpace(request.Name), x => EF.Functions.Like(x.Name, request.Name.ToLikePattern()));
    }
}
