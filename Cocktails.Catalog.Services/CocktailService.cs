using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Cocktails.Catalog.ViewModels;
using Cocktails.Data;
using Cocktails.Data.Domain;
using Cocktails.Mapper;

namespace Cocktails.Catalog.Services
{
    public class CocktailService : BaseService<Cocktail, CocktailModel>, ICocktailService
    {
        private readonly IRepository<Mix> _mixRepository;
        protected override Func<IQueryable<Cocktail>, IQueryable<Cocktail>> IncludeFunction =>
            x => x
                .Include(y => y.Mixes)
                .ThenInclude(y => y.Ingredient);

        public CocktailService(
            IRepository<Cocktail> cocktailRepository,
            IRepository<Mix> mixRepository,
            IModelMapper mapper)
            : base(cocktailRepository, mapper)
        {
            _mixRepository = mixRepository;
        }

        public async Task<CollectionWrapper<CocktailModel>> GetByCocktailNameAsync(string cocktailName,
            QueryContext context, CancellationToken cancellationToken)
        {
            var result = await Repository.GetAsync(
                x => GetQuery(context)(
                    IncludeFunction(x.Where(y => y.Name.Contains(cocktailName)))),
                cancellationToken);
            return WrapCollection(Mapper.Map<CocktailModel[]>(result), context);
        }

        public override async Task<CocktailModel> UpdateAsync(Guid id, CocktailModel model, CancellationToken cancellationToken)
        {
            await ProcessMixesAsync(id, model, cancellationToken);
            var result = await base.UpdateAsync(id, model, cancellationToken);
            if (result != null)
            {
                await _mixRepository.CommitAsync(cancellationToken);
                return await GetByIdAsync(result.Id, cancellationToken);
            }
            return null; // TODO handle null?
        }

        private async Task ProcessMixesAsync(Guid id, CocktailModel model, CancellationToken cancellationToken)
        {
            var mixes = await _mixRepository.GetAsync(x => x.Where(y => y.Id == id), cancellationToken);
            foreach (var mix in mixes)
            {
                var modelMix = model.Mixes.FirstOrDefault(x => x.IngredientId == mix.IngredientId);
                if (modelMix != null)
                {
                    if (modelMix.Amount != mix.Amount)
                    {
                        mix.Amount = modelMix.Amount;
                        await _mixRepository.UpdateAsync(mix, cancellationToken);
                    }
                    model.Mixes.Remove(modelMix);
                }
                else
                {
                    await _mixRepository.DeleteAsync(mix, cancellationToken);
                }
            }
        }
    }
}
