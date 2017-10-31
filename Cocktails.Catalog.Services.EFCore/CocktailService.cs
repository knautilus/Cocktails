using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Cocktails.Catalog.ViewModels;
using Cocktails.Data;
using Cocktails.Data.Domain;
using Cocktails.Mapper;

namespace Cocktails.Catalog.Services.EFCore
{
    public class CocktailService : BaseService<Cocktail, CocktailModel>, ICocktailService
    {
        private readonly IRepository<Mix> _mixRepository;
        protected override Func<IQueryable<Cocktail>, IQueryable<Cocktail>> IncludeFunction =>
            QueryFunctions.CocktailsIncludeFunction;

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
                x => 
                    IncludeFunction(QueryFunctions.CocktailsByNameFunction(x, cocktailName))
                    .Paginate(context, y => y.ModifiedDate),
                cancellationToken);
            return WrapCollection(Mapper.Map<CocktailModel[]>(result), context);
        }

        public override async Task<CocktailModel> UpdateAsync(Guid id, CocktailModel model, CancellationToken cancellationToken)
        {
            await ProcessMixesAsync(id, model, cancellationToken);
            return await base.UpdateAsync(id, model, cancellationToken);
        }

        private async Task ProcessMixesAsync(Guid id, CocktailModel model, CancellationToken cancellationToken)
        {
            var mixes = await _mixRepository.GetAsync(x => QueryFunctions.GetByIdFunction<Mix>()(x, id), cancellationToken);
            foreach (var mix in mixes)
            {
                var modelMix = model.Mixes.FirstOrDefault(x => x.IngredientId == mix.IngredientId);
                if (modelMix != null)
                {
                    if (modelMix.Amount != mix.Amount)
                    {
                        mix.Amount = modelMix.Amount;
                        await _mixRepository.UpdateAsync(mix, cancellationToken, false);
                    }
                    model.Mixes.Remove(modelMix);
                }
                else
                {
                    await _mixRepository.DeleteAsync(mix, cancellationToken, false);
                }
            }
        }
    }
}
