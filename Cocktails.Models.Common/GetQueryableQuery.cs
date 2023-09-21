using MediatR;

namespace Cocktails.Models.Common
{
    public class GetQueryableQuery<TEntity> : IRequest<IQueryable<TEntity>>
    {
    }
}
