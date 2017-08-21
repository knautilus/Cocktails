using System;
using System.Collections.Generic;
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
    public class IngredientService : BaseService<Ingredient, IngredientModel>
    {
        public IngredientService(IRepository<Ingredient> repository, IModelMapper mapper)
            : base(repository, mapper) {}

        public override async Task<IngredientModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetSingleAsync(x => x
                .Where(y => y.Id == id)
                .Include(y => y.Flavor)
                .Include(y => y.Category), cancellationToken);
            return _mapper.Map<IngredientModel>(result);
        }

        public override async Task<IEnumerable<IngredientModel>> GetAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(x => x
                .Include(y => y.Flavor)
                .Include(y => y.Category), cancellationToken);
            return _mapper.Map<IEnumerable<IngredientModel>>(result);
        }
    }
}
