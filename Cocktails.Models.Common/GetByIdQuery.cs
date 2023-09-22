namespace Cocktails.Models.Common
{
    public class GetByIdQuery<TKey> : IQuery
    {
        public TKey Id { get; set; }
    }
}
