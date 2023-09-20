using Cocktails.Data.EFCore.Extensions;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Ingredients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.QueryHandlers
{
    public class IngredientGetManyQueryHandler : IRequestHandler<IngredientGetManyQuery, IQueryable<Ingredient>>
    {
        private readonly DbContext _dbContext;

        public IngredientGetManyQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IQueryable<Ingredient>> Handle(IngredientGetManyQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Set<Ingredient>()
                .ConditionalWhere(!string.IsNullOrWhiteSpace(request.Name), x => EF.Functions.Like(x.Name, request.Name.ToLikePattern())));
        }
    }
}
