using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;

namespace Cocktails.Services
{
    public class CocktailService : BaseService<Cocktail>
    {
        private readonly IRepository<Mix> _mixRepository;

        public CocktailService(
            IRepository<Cocktail> cocktailRepository,
            IRepository<Mix> mixRepository)
            : base(cocktailRepository)
        {
            _mixRepository = mixRepository;
        }

        public override async Task<Cocktail> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetSingleAsync(x => x.Where(y => y.Id == id).Include(y => y.Mixes), cancellationToken);
            return result;
        }

        public override async Task<Cocktail> UpdateAsync(Guid id, Cocktail model, CancellationToken cancellationToken)
        {
            await ProcessMixesAsync(id, model, cancellationToken);
            var result = await base.UpdateAsync(id, model, cancellationToken);
            if (result != null)
            {
                await _mixRepository.CommitAsync(cancellationToken);
            }
            return result;
        }

        private async Task ProcessMixesAsync(Guid id, Cocktail model, CancellationToken cancellationToken)
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
