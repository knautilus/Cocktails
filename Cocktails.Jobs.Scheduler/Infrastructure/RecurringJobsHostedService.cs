using Cocktails.Jobs.Scheduler.Recurring;
using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cocktails.Jobs.Scheduler.Infrastructure
{
    internal class RecurringJobsHostedService : BackgroundService
    {
        private readonly IEnumerable<IRecurringJob> _jobs;
        private readonly ILogger<RecurringJobsHostedService> _logger;
        private readonly IRecurringJobManager _recurringJobManager;

        public RecurringJobsHostedService(
            IEnumerable<IRecurringJob> jobs,
            ILogger<RecurringJobsHostedService> logger,
            IRecurringJobManager recurringJobManager)
        {
            _jobs = jobs;
            _logger = logger;
            _recurringJobManager = recurringJobManager;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (var job in _jobs)
            {
                var jobTypeName = job.GetType().Name;

                if (job.Enabled)
                {
                    _recurringJobManager.AddOrUpdate(jobTypeName, () => job.RunAsync(stoppingToken),
                        !string.IsNullOrWhiteSpace(job.CronUtcExpression) ? job.CronUtcExpression : Cron.Never());
                }
                else
                {
                    _logger.LogInformation($"[{jobTypeName}] Не будет запущена. Отключена в настройках.");
                    _recurringJobManager.RemoveIfExists(recurringJobId: jobTypeName);
                }
            }

            return Task.CompletedTask;
        }
    }
}