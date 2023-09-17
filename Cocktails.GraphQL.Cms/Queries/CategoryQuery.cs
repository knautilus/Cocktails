using Cocktails.Data.Entities;
using Cocktails.Models.Cms.Requests;
using Cocktails.Models.Cms.Requests.Categories;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class CategoryQuery
    {
        public async Task<IQueryable<CocktailCategory>> GetCategories(CategoryGetManyQuery request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }

        public async Task<CocktailCategory> GetCategory(GetByIdQuery<long, CocktailCategory> request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
    }
}
