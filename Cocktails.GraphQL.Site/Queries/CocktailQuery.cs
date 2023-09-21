using Cocktails.Entities.Elasticsearch;
using Cocktails.Models.Common;
using Cocktails.Models.Site.Requests.Cocktails;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace Cocktails.GraphQL.Site.Queries
{
    [ExtendObjectType("rootQuery")]
    public class CocktailQuery
    {
        public async Task<CocktailDocument[]> GetCocktails(GetManyQuery<CocktailDocument, CocktailSort> request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send<CocktailDocument[]>(request, cancellationToken);
        }

        public async Task<CocktailDocument> GetCocktail(GetByIdQuery<long, CocktailDocument> request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
    }
}
