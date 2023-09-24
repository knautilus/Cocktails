using Cocktails.Entities.Elasticsearch;

namespace Cocktails.Data.Elasticsearch.Configuration
{
    public class ElasticIndexConfiguration : IIndexConfiguration
    {
        private readonly Dictionary<Type, string> _indexDictionary = new()
        {
            { typeof(CocktailDocument), IndexNames.CocktailsIndex }
        };

        public string GetIndexName<T>()
        {
            return _indexDictionary[typeof(T)];
        }
    }
}
