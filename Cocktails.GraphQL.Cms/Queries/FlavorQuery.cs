using Cocktails.Data.Entities;
using Cocktails.Models.Cms.Requests;
using Cocktails.Models.Cms.Requests.Flavors;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace Flavors.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class FlavorQuery
    {
        public async Task<IQueryable<Flavor>> GetFlavors(FlavorGetManyQuery request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }

        public async Task<Flavor> GetFlavor(GetByIdQuery<long, Flavor> request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
    }
}
