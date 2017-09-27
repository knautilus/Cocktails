using System;
using System.Linq;
using Cocktails.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Catalog.Services.EFCore
{
    public class QueryFunctions
    {
        public static Func<IQueryable<T>, Guid, IQueryable<T>> GetByIdFunction<T>() where T : BaseEntity =>
            (x, id) => x.Where(y => y.Id == id);

        public static Func<IQueryable<Cocktail>, IQueryable<Cocktail>> CocktailsIncludeFunction =
            x => x
                .Include(y => y.Mixes)
                .ThenInclude(y => y.Ingredient);

        public static Func<IQueryable<Cocktail>, string, IQueryable<Cocktail>> CocktailsByNameFunction =
            (x, name) => x.Where(y => y.Name.Contains(name));

        public static Func<IQueryable<Ingredient>, IQueryable<Ingredient>> IngredientsIncludeFunction =
            x => x
                .Include(y => y.Flavor)
                .Include(y => y.Category);

        public static Func<IQueryable<Ingredient>, Guid, IQueryable<Ingredient>> IngredientsByCategoryIdFunction =
            (x, categoryId) => x.Where(y => y.CategoryId == categoryId);
    }
}
