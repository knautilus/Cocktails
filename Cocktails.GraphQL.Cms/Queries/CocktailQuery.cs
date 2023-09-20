using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests;
using Cocktails.Models.Cms.Requests.Cocktails;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class CocktailQuery
    {
        public async Task<IQueryable<Cocktail>> GetCocktails(CocktailGetManyQuery? request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request ?? new CocktailGetManyQuery(), cancellationToken);
        }

        public async Task<Cocktail> GetCocktail(GetByIdQuery<long, Cocktail> request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
    }
}
