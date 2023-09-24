using Cocktails.Common.Models;
using Cocktails.Cqrs.Mediator;
using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Cqrs.Nosql.QueryHandlers.Cocktails;
using Cocktails.Data.Elasticsearch;
using Cocktails.Data.Elasticsearch.Extensions;
using Cocktails.GraphQL.Site.Types;
using Cocktails.Models.Site.Requests.Cocktails;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;
using Cocktails.Data.Elasticsearch.Configuration;

var contentRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

var builder = WebApplication.CreateBuilder(new WebApplicationOptions { ContentRootPath = contentRootPath });

// Add services to the container.

//var msSqlConnectionString = builder.Configuration.GetConnectionString("MsSqlConnection");

builder.Services.AddControllers();

//builder.Services.AddDbContext<CocktailsContext>(
//    options => options.UseSqlServer(msSqlConnectionString));

//builder.Services.AddTransient(typeof(DbContext), typeof(CocktailsContext));

builder.Services
    .AddGraphQLServer()
    //.AddSorting()
    //.SetPagingOptions(new PagingOptions { IncludeTotalCount = true })
    //.RegisterDbContext<CocktailsContext>(DbContextKind.Synchronized)
    .AddQueryType(d => d.Name("rootQuery"))
        .AddTypeExtension<CocktailQueryType>()
    .AllowIntrospection(builder.Environment.EnvironmentName == "Development");

builder.Services.AddMediator(typeof(CocktailGetManyQueryHandler));
builder.Services.AddTransient<IQueryHandler<CocktailGetManyQuery, int>, CocktailGetCountQueryHandler>();

var elasticSettings = builder.Configuration.GetSection("ElasticSettings").Get<ElasticSettings>();
builder.Services.AddElasticClient<ElasticIndexConfiguration>(elasticSettings);

var apiInfo = new ApiInfo { Name = "Cocktails Site Api", Author = "Alex Utiansky" };
builder.Services.AddSingleton(typeof(ApiInfo), x => apiInfo);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGraphQL().WithOptions(new GraphQLServerOptions
    {
        Tool = {
            UseBrowserUrlAsGraphQLEndpoint = false,
            Enable = app.Environment.IsDevelopment()
        }
    });
});

app.Run();
