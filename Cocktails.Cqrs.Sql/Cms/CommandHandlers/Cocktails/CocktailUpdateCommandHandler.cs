using AutoMapper;
using Cocktails.Common.Extensions;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Cocktails;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cocktails.Cqrs.Sql.Cms.CommandHandlers.Cocktails
{
    public class CocktailUpdateCommandHandler : IRequestHandler<CocktailUpdateCommand, long>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public CocktailUpdateCommandHandler(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<long> Handle(CocktailUpdateCommand request, CancellationToken cancellationToken)
        {
            var oldEntity = await _dbContext.Set<Cocktail>().AsNoTracking()
                .Where(y => y.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (oldEntity == null)
            {
                throw new Exception("Not found");
            }

            using var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted);

            try
            {
                await UpdateCocktail(request, _dbContext, _mapper, oldEntity, cancellationToken);
                await UpdateMixes(request, _dbContext, _mapper, cancellationToken);

                transaction.Commit();

                return request.Id;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        private static async Task UpdateCocktail(CocktailUpdateCommand request, DbContext dbContext, IMapper mapper, Cocktail oldEntity, CancellationToken cancellationToken)
        {
            mapper.Map(request, oldEntity);
            dbContext.Set<Cocktail>().Update(oldEntity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        private static async Task UpdateMixes(CocktailUpdateCommand request, DbContext dbContext, IMapper mapper, CancellationToken cancellationToken)
        {
            var newMixes = mapper.Map<Mix[]>(request.Mixes);

            foreach (var mix in newMixes)
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