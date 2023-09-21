using MediatR;

namespace Cocktails.Models.Common
{
    public class GetByIdQuery<TKey, TResponse> : IRequest<TResponse>
    {
        public TKey Id { get; set; }
    }
}
