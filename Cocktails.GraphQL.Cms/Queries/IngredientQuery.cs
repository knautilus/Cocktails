using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests;
using Cocktails.Models.Cms.Requests.Ingredients;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class IngredientQuery
    {
        public async Task<IQueryable<Ingredient>> GetIngredients(IngredientGetManyQuery? request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request ?? new IngredientGetManyQuery(), cancellationToken);
        }

        public async Task<Ingredient> GetIngredient(GetByIdQuery<long, Ingredient> request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
    }
}
