using Cocktails.Data.EFCore.Extensions;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.CocktailCategories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.QueryHandlers
{
    public class CocktailCategoryGetManyQueryHandler : IRequestHandler<CocktailCategoryGetManyQuery, IQueryable<CocktailCategory>>
    {
        private readonly DbContext _dbContext;

        public CocktailCategoryGetManyQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IQueryable<CocktailCategory>> Handle(CocktailCategoryGetManyQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Set<CocktailCategory>()
                .ConditionalWhere(!string.IsNullOrWhiteSpace(request.Name), x => EF.Functions.Like(x.Name, request.Name.ToLikePattern())));
        }
    }
}
