using Cocktails.Data.EFCore.Extensions;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Cocktails;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.QueryHandlers.Cocktails
{
    public class CocktailGetManyQueryHandler : IRequestHandler<CocktailGetManyQuery, IQueryable<Cocktail>>
    {
        private readonly DbContext _dbContext;

        public CocktailGetManyQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IQueryable<Cocktail>> Handle(CocktailGetManyQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Set<Cocktail>()
                .ConditionalWhere(!string.IsNullOrWhiteSpace(request.Name), x => EF.Functions.Like(x.Name, request.Name.ToLikePattern()))
                .ConditionalWhere(request.IngredientId.HasValue, x => x.Mixes.Any(m => m.IngredientId == request.IngredientId.Value))
                .ConditionalWhere(request.CocktailCategoryId.HasValue, x => x.CocktailCategoryId == request.CocktailCategoryId.Value)
                .ConditionalWhere(request.FlavorId.HasValue, x => x.CocktailCategoryId == request.FlavorId.Value));
        }
    }
}
