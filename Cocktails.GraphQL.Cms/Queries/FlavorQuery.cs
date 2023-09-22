using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Flavors;
using Cocktails.Models.Common;
using HotChocolate;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class FlavorQuery
    {
        public async Task<IQueryable<Flavor>> GetFlavors(FlavorGetQueryableQuery? request, [Service] IQueryProcessor queryProcessor, CancellationToken cancellationToken)
        {
            return await queryProcessor.Process<IQueryable<Flavor>>(request ?? new FlavorGetQueryableQuery(), cancellationToken);
        }

        public async Task<Flavor> GetFlavor(GetByIdQuery<long> request, [Service] IQueryProcessor queryProcessor, CancellationToken cancellationToken)
        {
            return await queryProcessor.Process<Flavor>(request, cancellationToken);
        }
    }
}
