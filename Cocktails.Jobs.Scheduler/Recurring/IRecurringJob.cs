using Hangfire;

namespace Cocktails.Jobs.Scheduler.Recurring
{
    public interface IRecurringJob
    {
        Task RunAsync(CancellationToken cancellationToken);

        bool Enabled { get; }

        string CronUtcExpression { get; }
    }
}
