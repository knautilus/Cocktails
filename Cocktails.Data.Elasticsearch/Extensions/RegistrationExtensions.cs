using Elasticsearch.Net;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System.Collections.Specialized;

namespace Cocktails.Data.Elasticsearch.Extensions
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddElasticClient<TIndexConfiguration>(
            this IServiceCollection container, ElasticSettings settings)
        {
            var connectionSettings = GetConnectionSettings(settings);

            container.AddScoped<IElasticClient>(x => new ElasticClient(connectionSettings));
            container.AddSingleton(typeof(IIndexConfiguration), typeof(TIndexConfiguration));

            return container;
        }

        public static ConnectionSettings GetConnectionSettings(ElasticSettings settings)
        {
            var nodes = settings.NodesList.Split(',');
            var pool = nodes.Length == 1
                ? new SingleNodeConnectionPool(new Uri(nodes.First())) as IConnectionPool
                : new StaticConnectionPool(nodes.Select(n => new Uri(n)));
            var headers = new NameValueCollection { { settings.AuthHeaderKey, settings.AuthHeaderValue } };
            var connectionSettings = new ConnectionSettings(pool)
                .GlobalHeaders(headers)
                .RequestTimeout(TimeSpan.FromMinutes(5))
                .DefaultFieldNameInferrer(x => x)
                .DisableDirectStreaming()
                .DisablePing();
            return connectionSettings;
        }
    }
}
