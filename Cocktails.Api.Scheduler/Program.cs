using Cocktails.Api.Scheduler.Configuration;
using Cocktails.Cqrs.Sql.Scheduler.QueryHandlers.Cocktails;
using Cocktails.Data.Contexts;
using Cocktails.Jobs.Scheduler.Extensions;
using Cocktails.Mapper.Common;
using Cocktails.Mapper.Scheduler;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var contentRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

var builder = WebApplication.CreateBuilder(new WebApplicationOptions { ContentRootPath = contentRootPath });

// Add services to the container.

var msSqlConnectionString = builder.Configuration.GetConnectionString("MsSqlConnection");

builder.Services.AddDbContext<CocktailsContext>(
    options => options.UseSqlServer(msSqlConnectionString));

builder.Services.AddTransient(typeof(DbContext), typeof(CocktailsContext));

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


builder.Services.AddAutoMapper<SchedulerMapperConfiguration>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<BuildCocktailDocumentsQueryHandler>());

builder.Services.AddSchedulerJobs(builder.Configuration.GetSection("Jobs"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseHangfireDashboard();

app.Run();
