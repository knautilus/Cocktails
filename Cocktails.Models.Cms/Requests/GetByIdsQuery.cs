using MediatR;

namespace Cocktails.Models.Cms.Requests
{
    public class GetByIdsQuery<TKey, TResponse> : IRequest<TResponse>
    {
        public TKey[] Ids { get; set; }
    }
}
