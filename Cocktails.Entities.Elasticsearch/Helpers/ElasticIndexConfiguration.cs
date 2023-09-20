namespace Cocktails.Entities.Elasticsearch.Helpers
{
    public class ElasticIndexConfiguration : IIndexConfiguration
    {
        private readonly Dictionary<Type, string> _indexDictionary = new()
        {
            //{ typeof(ArticleDocument), IndexNames.ArticleIndex }
        };

        public string GetIndexName<T>()
        {
            return _indexDictionary[typeof(T)];
        }
    }
}
