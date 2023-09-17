using Cocktails.Models.Cms.Requests.Cocktails;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace Cocktails.GraphQL.Cms.Mutations
{
    [ExtendObjectType("rootMutation")]
    public class CocktailMutation
    {
        public async Task<long> CreateCocktail(CocktailCreateCommand request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            var date = DateTimeOffset.UtcNow;

            request.CreateDate = request.ModifyDate = date;
            request.CreateUserId = request.ModifyUserId = 1;

            return await mediator.Send(request, cancellationToken);
        }

        public async Task<long> UpdateCocktail(CocktailUpdateCommand request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            var date = DateTimeOffset.UtcNow;

            request.ModifyDate = date;
            request.ModifyUserId = 1;

            return await mediator.Send(request, cancellationToken);
        }
    }
}
