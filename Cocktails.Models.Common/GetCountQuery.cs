using MediatR;

namespace Cocktails.Models.Common
{
    public class GetCountQuery<TEntity> : IRequest<int>
    {

    }
}
