using Cocktails.Jobs.Scheduler.Recurring;
using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.States;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Cocktails.Jobs.Scheduler.Infrastructure
{
    internal class HangfireFilter : IJobFilter, IClientFilter, IServerFilter, IElectStateFilter
    {
        private readonly ConcurrentDictionary<Type, ILogger> _loggers = new();

        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _serviceProvider;

        public HangfireFilter(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            _serviceProvider = serviceProvider;
            _loggerFactory = loggerFactory;
        }

        private ILogger GetLogger(Type type)
        {
            return _loggers.GetOrAdd(type, _loggerFactory.CreateLogger);
        }

        public void OnCreating(CreatingContext context)
        {
            GetLogger(context.Job.Type).LogInformation("Creating a job based on `{0}`...", context.Job.Type.Name);
        }

        public void OnCreated(CreatedContext context)
        {
            GetLogger(context.Job.Type)
                .LogInformation($"Job that is based on `{context.Job.Type.Name}` has been created with id `{context.BackgroundJob?.Id}`");
        }

        public void OnPerforming(PerformingContext context)
        {
            if (typeof(IRecurringJob).IsAssignableFrom(context.BackgroundJob.Job.Type))
            {
                var job = (IRecurringJob)ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, context.BackgroundJob.Job.Type);
                if (!job.Enabled)
                {
                    context.Canceled = true;
                    GetLogger(context.BackgroundJob.Job.Type)
                        .LogInformation($"[{context.BackgroundJob.Job.Type.Name}] Не будет запущена. Отключена в настройках.");

                    return;
                }
            }

            //if (typeof(IBackgroundJob).IsAssignableFrom(context.BackgroundJob.Job.Type))
            //{
            //    var job = (IBackgroundJob)ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, context.BackgroundJob.Job.Type);
            //    if (!job.Enabled)
            //    {
            //        context.Canceled = true;
            //        GetLogger(context.BackgroundJob.Job.Type)
            //            .LogInformation($"[{context.BackgroundJob.Job.Type.Name}] Не будет запущена. Отключена в настройках.");

            //        return;
            //    }
            //}

            GetLogger(context.BackgroundJob.Job.Type).LogInformation($"[{context.BackgroundJob.Job.Type.Name}] Начала работу.");
        }

        public void OnPerformed(PerformedContext context)
        {
            GetLogger(context.BackgroundJob.Job.Type).LogInformation($"[{context.BackgroundJob.Job.Type.Name}] Закончила работу.");
        }

        public void OnStateElection(ElectStateContext context)
        {
            if (context.CandidateState is FailedState failedState)
            {
                GetLogger(context.BackgroundJob.Job.Type)
                    .LogError(failedState.Exception, $"[{context.BackgroundJob.Job.Type.Name}] Закончила работу с ошибкой");
            }
        }

        public bool AllowMultiple { get; } = false;

        public int Order { get; } = -1;
    }
}
