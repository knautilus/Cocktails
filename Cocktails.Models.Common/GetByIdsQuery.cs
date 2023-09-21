using MediatR;

namespace Cocktails.Models.Common
{
    public class GetByIdsQuery<TKey, TResponse> : IRequest<TResponse[]>
    {
        public TKey[] Ids { get; set; }
    }
}
