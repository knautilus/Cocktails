using MediatR;

namespace Cocktails.Models.Cms.Requests
{
    public class GetByIdQuery<TKey, TResponse> : IRequest<TResponse>
    {
        public TKey Id { get; set; }
    }
}
