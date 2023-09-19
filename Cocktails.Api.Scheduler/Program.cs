using Cocktails.Api.Scheduler.Configuration;
using Cocktails.Jobs.Scheduler.Extensions;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var contentRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

var builder = WebApplication.CreateBuilder(new WebApplicationOptions { ContentRootPath = contentRootPath });

// Add services to the container.

var msSqlConnectionString = builder.Configuration.GetConnectionString("MsSqlConnection");

var hangfireSettings = builder.Configuration.GetSection("HangfireSettings").Get<HangfireSettings>();
builder.Services.AddHangfire(configuration =>
{
    configuration.UseSqlServerStorage(hangfireSettings.StorageConnectionString, new SqlServerStorageOptions
    {
        TransactionTimeout = TimeSpan.FromHours(1),
        CommandBatchMaxTimeout = TimeSpan.FromHours(1),
        CommandTimeout = TimeSpan.FromHours(1),
        SlidingInvisibilityTimeout = TimeSpan.FromHours(1),
        SchemaName = hangfireSettings.StorageSchemaName
    });
});
GlobalJobFilters.Filters.Add(new DisableConcurrentExecutionAttribute(10));

builder.Services.AddHangfireServer(options =>
{
    options.CancellationCheckInterval = TimeSpan.FromSeconds(5);
});

builder.Services.AddSchedulerJobs(builder.Configuration.GetSection("Jobs"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseHangfireDashboard();

var cancellationTokenSource = new CancellationTokenSource();

app.Run();
