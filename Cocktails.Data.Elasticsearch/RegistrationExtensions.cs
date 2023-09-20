using Elasticsearch.Net;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System.Collections.Specialized;

namespace Cocktails.Data.Elasticsearch
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddElasticClient(
            this IServiceCollection container, ElasticSettings settings, bool singleton = false)
        {
            var connectionSettings = GetConnectionSettings(settings);

            if (singleton)
            {
                container.AddSingleton<IElasticClient>(new ElasticClient(connectionSettings));
            }
            else
            {
                container.AddScoped<IElasticClient>(x => new ElasticClient(connectionSettings));
            }
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
