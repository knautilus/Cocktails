using Cocktails.Jobs.Scheduler.Infrastructure;
using Cocktails.Jobs.Scheduler.Recurring;
using Hangfire.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cocktails.Jobs.Scheduler.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSchedulerJobs(this IServiceCollection services, IConfigurationSection jobsConfiguration)
        {
            services.AddHangfireJobs()
                .AddRecurringJob<TestRecurringJob>()
                .AddRecurringJob<UpdateElasticCocktailsIndexJob>();
        }

        private static HangfireJobsBuilder AddHangfireJobs(this IServiceCollection services)
        {
            services.AddSingleton<IJobFilter, HangfireFilter>();

            services.AddHostedService<RecurringJobsHostedService>();
            //services.AddHostedService<BackgroundJobsHostedService>();

            return new HangfireJobsBuilder(services);
        }
    }
}
