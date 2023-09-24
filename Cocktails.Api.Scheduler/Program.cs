using Cocktails.Api.Scheduler.Configuration;
using Cocktails.Cqrs.Mediator;
using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Cqrs.Sql;
using Cocktails.Cqrs.Sql.Scheduler.QueryHandlers.Cocktails;
using Cocktails.Data.Elasticsearch;
using Cocktails.Entities.Sql;
using Cocktails.Jobs.Scheduler.Extensions;
using Cocktails.Mapper.Common;
using Cocktails.Mapper.Scheduler;
using Cocktails.Models.Common;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Cocktails.Data.EFCore.DbContexts;
using Cocktails.Data.Elasticsearch.Configuration;
using Cocktails.Data.Elasticsearch.Extensions;

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

builder.Services.AddMediator(typeof(BuildCocktailDocumentsQueryHandler));
builder.Services.AddTransient<IQueryHandler<GetByIdsQuery<long>, CocktailCategory[]>, GetByIdsQueryHandler<long, CocktailCategory>>();
builder.Services.AddTransient<IQueryHandler<GetByIdsQuery<long>, Flavor[]>, GetByIdsQueryHandler<long, Flavor>>();
builder.Services.AddTransient<IQueryHandler<GetByIdsQuery<long>, Ingredient[]>, GetByIdsQueryHandler<long, Ingredient>>();
builder.Services.AddTransient<IQueryHandler<GetByIdsQuery<long>, MeasureUnit[]>, GetByIdsQueryHandler<long, MeasureUnit>>();

var elasticSettings = builder.Configuration.GetSection("ElasticSettings").Get<ElasticSettings>();
builder.Services.AddElasticClient<ElasticIndexConfiguration>(elasticSettings);

builder.Services.AddSchedulerJobs(builder.Configuration.GetSection("Jobs"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseHangfireDashboard();

app.Run();
