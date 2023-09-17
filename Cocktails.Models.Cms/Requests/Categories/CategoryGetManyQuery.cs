using Cocktails.Data.Entities;
using MediatR;

namespace Cocktails.Models.Cms.Requests.Categories
{
    public class CategoryGetManyQuery : IRequest<IQueryable<CocktailCategory>>
    {
        public string Name { get; set; }
    }
}
