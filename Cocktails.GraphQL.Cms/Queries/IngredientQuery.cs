using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Ingredients;
using Cocktails.Models.Common;
using HotChocolate;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class IngredientQuery
    {
        public async Task<IQueryable<Ingredient>> GetIngredients(IngredientGetQueryableQuery? request, [Service] IQueryProcessor queryProcessor, CancellationToken cancellationToken)
        {
            return await queryProcessor.Process<IQueryable<Ingredient>>(request ?? new IngredientGetQueryableQuery(), cancellationToken);
        }

        public async Task<Ingredient> GetIngredient(GetByIdQuery<long> request, [Service] IQueryProcessor queryProcessor, CancellationToken cancellationToken)
        {
            return await queryProcessor.Process<Ingredient>(request, cancellationToken);
        }
    }
}
