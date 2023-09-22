namespace Cocktails.Models.Common
{
    public class GetByIdsQuery<TKey> : IQuery
    {
        public TKey[] Ids { get; set; }
    }
}
