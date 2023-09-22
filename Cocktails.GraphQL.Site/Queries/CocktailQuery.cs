using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Entities.Elasticsearch;
using Cocktails.Models.Site.Requests.Cocktails;
using HotChocolate;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Site.Queries
{
    [ExtendObjectType("rootQuery")]
    public class CocktailQuery
    {
        public async Task<CocktailDocument[]> GetCocktails(CocktailGetManyQuery request, [Service] IQueryProcessor queryProcessor, CancellationToken cancellationToken)
        {
            return await queryProcessor.Process<CocktailDocument[]>(request, cancellationToken);
        }

        public async Task<int> GetCocktailsCount(CocktailGetManyQuery request, [Service] IQueryProcessor queryProcessor, CancellationToken cancellationToken)
        {
            return await queryProcessor.Process<int>(request, cancellationToken);
        }

        //public async Task<CocktailDocument> GetCocktail(GetByIdQuery<long, CocktailDocument> request, [Service] IMediator queryProcessor, CancellationToken cancellationToken)
        //{
        //    return await queryProcessor.Send(request, cancellationToken);
        //}
    }
}
