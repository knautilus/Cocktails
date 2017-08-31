using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;
using Cocktails.Mapper;
using Cocktails.ViewModels;

namespace Cocktails.Services
{
    public class CocktailService : BaseService<Cocktail, CocktailModel>
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

        public override async Task<CocktailModel> UpdateAsync(Guid id, CocktailModel model, CancellationToken cancellationToken)
        {
            await ProcessMixesAsync(id, model, cancellationToken);
            var result = await base.UpdateAsync(id, model, cancellationToken);
            if (result != null)
            {
                await _mixRepository.CommitAsync(cancellationToken);
            }
            return await GetByIdAsync(result.Id, cancellationToken);
        }

        private async Task ProcessMixesAsync(Guid id, CocktailModel model, CancellationToken cancellationToken)
        {
            var mixes = await _mixRepository.GetAsync(x => x.Where(y => y.Id == id), cancellationToken);
            if (mixes.Any())
            {
                foreach (Mix mix in mixes)
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
}
