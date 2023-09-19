using Hangfire.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Cocktails.Jobs.Scheduler.Infrastructure
{
    public class ServiceCollectionJobFilterProvider : IJobFilterProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceCollectionJobFilterProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<JobFilter> GetFilters(Job job)
        {
            var ijobs = _serviceProvider.GetRequiredService<IEnumerable<IJobFilter>>();

            return ijobs.Select(x => new JobFilter(x, JobFilterScope.Global, x.Order));
        }
    }
}
