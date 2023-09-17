using AutoMapper;
using Cocktails.Common.Extensions;
using Cocktails.Data.Contexts;
using Cocktails.Data.Entities;
using Cocktails.Models.Cms.Requests.Cocktail;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cocktails.GraphQL.Cms.Mutations
{
    [ExtendObjectType("rootMutation")]
    public class CocktailMutation
    {
        public async Task<long> CreateCocktail(CocktailCreateCommand request, CocktailsContext dbContext, [Service] IMapper mapper, CancellationToken cancellationToken)
        {
            var date = DateTimeOffset.UtcNow;

            request.CreateDate = request.ModifyDate = date;
            request.CreateUserId = request.ModifyUserId = 1;

            var entity = mapper.Map<Cocktail>(request);

            await dbContext.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<long> UpdateCocktail(CocktailUpdateCommand request, CocktailsContext dbContext, [Service] IMapper mapper, CancellationToken cancellationToken)
        {
            var date = DateTimeOffset.UtcNow;

            request.ModifyDate = date;
            request.ModifyUserId = 1;

            var oldEntity = await dbContext.Set<Cocktail>().AsNoTracking()
                .Where(y => y.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (oldEntity == null)
            {
                throw new Exception("Not found");
            }

            using var transaction = dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted);

            try
            {
                await UpdateCocktail(request, dbContext, mapper, oldEntity, cancellationToken);
                await UpdateMixes(request, dbContext, mapper, cancellationToken);

                transaction.Commit();

                return request.Id;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        private static async Task UpdateCocktail(CocktailUpdateCommand request, CocktailsContext dbContext, IMapper mapper, Cocktail oldEntity, CancellationToken cancellationToken)
        {
            mapper.Map(request, oldEntity);
            dbContext.Set<Cocktail>().Update(oldEntity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        private static async Task UpdateMixes(CocktailUpdateCommand request, CocktailsContext dbContext, IMapper mapper, CancellationToken cancellationToken)
        {
            var newMixes = mapper.Map<Mix[]>(request.Mixes);

            foreach(var mix in newMixes)
            {
                mix.CocktailId = request.Id;
            }

            var oldMixes = await dbContext.Set<Mix>().AsNoTracking()
                .Where(x => x.CocktailId == request.Id)
                .ToArrayAsync(cancellationToken);

            var mixesToRemove = oldMixes.Except(newMixes, x => new { x.CocktailId, x.IngredientId }).ToArray();
            var mixesToAdd = newMixes.Except(oldMixes, x => new { x.CocktailId, x.IngredientId }).ToArray();
            var mixesToUpdate = oldMixes.Except(mixesToRemove, x => new { x.CocktailId, x.IngredientId }).ToArray();

            dbContext.Set<Mix>().RemoveRange(mixesToRemove);

            await dbContext.Set<Mix>().AddRangeAsync(mixesToAdd, cancellationToken);

            foreach (var updateItem in mixesToUpdate)
            {
                var mix = newMixes
                    .FirstOrDefault(w => w.IngredientId == updateItem.IngredientId);

                if (mix != null)
                {
                    mapper.Map(mix, updateItem);
                }
            }
            dbContext.Set<Mix>().UpdateRange(mixesToUpdate);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
