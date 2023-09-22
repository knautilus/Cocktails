using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.CocktailCategories;
using Cocktails.Models.Common;
using HotChocolate;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class CocktailCategoryQuery
    {
        public async Task<IQueryable<CocktailCategory>> GetCategories(CocktailCategoryGetQueryableQuery? request, [Service] IQueryProcessor queryProcessor, CancellationToken cancellationToken)
        {
            return await queryProcessor.Process<IQueryable<CocktailCategory>>(request ?? new CocktailCategoryGetQueryableQuery(), cancellationToken);
        }

        public async Task<CocktailCategory> GetCategory(GetByIdQuery<long> request, [Service] IQueryProcessor queryProcessor, CancellationToken cancellationToken)
        {
            return await queryProcessor.Process<CocktailCategory>(request, cancellationToken);
        }
    }
}
