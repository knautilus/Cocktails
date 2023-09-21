namespace Cocktails.Models.Common
{
    public class BuildDocumentsQuery<TKey, TDocument> : GetManyQuery<TDocument, int>
    {
        public TKey[] Ids { get; set; }
    }
}
