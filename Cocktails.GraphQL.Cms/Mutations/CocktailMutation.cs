using Cocktails.Cqrs.Mediator.Commands;
using Cocktails.Models.Cms.Requests.Cocktails;
using HotChocolate;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Mutations
{
    [ExtendObjectType("rootMutation")]
    public class CocktailMutation
    {
        public async Task<long> CreateCocktail(CocktailCreateCommand request, [Service] ICommandProcessor commandProcessor, CancellationToken cancellationToken)
        {
            var date = DateTimeOffset.UtcNow;

            request.CreateDate = request.ModifyDate = date;
            request.CreateUserId = request.ModifyUserId = 1;

            return await commandProcessor.Process<long>(request, cancellationToken);
        }

        public async Task<long> UpdateCocktail(CocktailUpdateCommand request, [Service] ICommandProcessor commandProcessor, CancellationToken cancellationToken)
        {
            var date = DateTimeOffset.UtcNow;

            request.ModifyDate = date;
            request.ModifyUserId = 1;

            return await commandProcessor.Process<long>(request, cancellationToken);
        }
    }
}
