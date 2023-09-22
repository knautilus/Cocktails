using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Data.Elasticsearch;
using Cocktails.Entities.Common;
using Cocktails.Models.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nest;

namespace Cocktails.Jobs.Scheduler.Recurring
{
    public abstract class BaseUpdateElasticIndexJob<TKey, TDocument> : IRecurringJob
        where TDocument : BaseEntity<TKey>
    {
        protected virtual int DocumentsPortionSize => 500;

        protected virtual TKey[] ItemIds => null;

        private readonly IServiceScopeFactory _services;

        private readonly ILogger<BaseUpdateElasticIndexJob<TKey, TDocument>> _logger;

        protected BaseUpdateElasticIndexJob(ILogger<BaseUpdateElasticIndexJob<TKey, TDocument>> logger, IServiceScopeFactory services, bool enabled, string cronUtcExpression)
        {
            _logger = logger;
            _services = services;
            Enabled = enabled;
            CronUtcExpression = cronUtcExpression;
        }

        public virtual async Task RunAsync(CancellationToken cancellationToken)
        {
            try
            {
                await UpsertDocuments(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, e, $"{GetType()} failed: {e.Message}");
                throw;
            }
        }

        protected async Task UpsertDocuments(CancellationToken cancellationToken)
        {
            var index = 0;

            using var scope = _services.CreateScope();

            var elasticClient = scope.ServiceProvider.GetRequiredService<IElasticClient>();
            var indexConfiguration = scope.ServiceProvider.GetRequiredService<IIndexConfiguration>();
            var queryProcessor = scope.ServiceProvider.GetRequiredService<IQueryProcessor>();

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var documents = await queryProcessor.Process<TDocument[]>(
                    new BuildDocumentsQuery<TKey> { Take = DocumentsPortionSize, Skip = index, Ids = ItemIds },
                    cancellationToken);

                if (documents.Length == 0)
                {
                    break;
                }

                await elasticClient.IndexManyAsync(documents,
                    indexConfiguration.GetIndexName<TDocument>(), cancellationToken);

                index += DocumentsPortionSize;

                documents = null;
                GC.Collect();
            }
        }

        public bool Enabled { get; }
        public string CronUtcExpression { get; }
    }
}
