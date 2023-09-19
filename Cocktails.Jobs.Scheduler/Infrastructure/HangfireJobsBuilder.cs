using Cocktails.Jobs.Scheduler.Recurring;
using Cocktails.Jobs.Scheduler.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cocktails.Jobs.Scheduler.Infrastructure
{
    public class HangfireJobsBuilder
    {
        private readonly IServiceCollection _serviceCollection;

        internal HangfireJobsBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public HangfireJobsBuilder AddRecurringJob<TJob, TJobSettings>(IConfigurationSection jobsConfiguration)
            where TJob : class, IRecurringJob
            where TJobSettings : class, IJobSettings
        {
            _serviceCollection.AddSingleton<IRecurringJob, TJob>();

            var jobSettingsTypeName = typeof(TJobSettings).Name;
            var config = jobsConfiguration.GetSection(jobSettingsTypeName).Get<TJobSettings>();
            _serviceCollection.AddSingleton(config);

            return this;
        }

        public HangfireJobsBuilder AddRecurringJob<TJob>()
            where TJob : class, IRecurringJob
        {
            _serviceCollection.AddSingleton<IRecurringJob, TJob>();

            return this;
        }

        //public HangfireJobsBuilder AddBackgroundJob<TJob>()
        //    where TJob : class, IBackgroundJob
        //{
        //    _serviceCollection.AddSingleton<IBackgroundJob, TJob>();

        //    return this;
        //}
    }
}