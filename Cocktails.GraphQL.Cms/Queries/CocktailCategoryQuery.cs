using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests;
using Cocktails.Models.Cms.Requests.CocktailCategories;
using Cocktails.Models.Common;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class CocktailCategoryQuery
    {
        public async Task<IQueryable<CocktailCategory>> GetCategories(CocktailCategoryGetQueryableQuery? request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request ?? new CocktailCategoryGetQueryableQuery(), cancellationToken);
        }

        public async Task<CocktailCategory> GetCategory(GetByIdQuery<long, CocktailCategory> request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
    }
}
