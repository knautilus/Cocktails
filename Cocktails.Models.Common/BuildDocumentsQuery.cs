namespace Cocktails.Models.Common
{
    public class BuildDocumentsQuery<TKey> : GetManyQuery<int>
    {
        public TKey[] Ids { get; set; }
    }
}
