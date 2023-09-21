using Cocktails.Data.Elasticsearch;
using Cocktails.Entities.Elasticsearch.Constants;

namespace Cocktails.Entities.Elasticsearch.Helpers
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
