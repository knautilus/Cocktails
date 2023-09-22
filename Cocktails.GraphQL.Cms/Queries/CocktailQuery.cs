using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Cocktails;
using Cocktails.Models.Common;
using HotChocolate;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class CocktailQuery
    {
        public async Task<IQueryable<Cocktail>> GetCocktails(CocktailGetQueryableQuery? request, [Service] IQueryProcessor queryProcessor, CancellationToken cancellationToken)
        {
            return await queryProcessor.Process<IQueryable<Cocktail>>(request ?? new CocktailGetQueryableQuery(), cancellationToken);
        }

        public async Task<Cocktail> GetCocktail(GetByIdQuery<long> request, [Service] IQueryProcessor queryProcessor, CancellationToken cancellationToken)
        {
            return await queryProcessor.Process<Cocktail>(request, cancellationToken);
        }
    }
}
