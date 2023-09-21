using Cocktails.Entities.Elasticsearch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cocktails.Jobs.Scheduler.Recurring
{
    public class UpdateElasticCocktailsIndexJob : BaseUpdateElasticIndexJob<long, CocktailDocument>
    {
        public UpdateElasticCocktailsIndexJob(ILogger<UpdateElasticCocktailsIndexJob> logger, IServiceScopeFactory services) : base(logger, services, true, "*/10 * * * *")
        {
        }
    }
}
