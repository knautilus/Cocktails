using Cocktails.Catalog.Mapper;
using Cocktails.Common.Models;
using Cocktails.Cqrs.Sql;
using Cocktails.Cqrs.Sql.Cms.QueryHandlers.Cocktails;
using Cocktails.Data.Contexts;
using Cocktails.Data.Entities;
using Cocktails.GraphQL.Cms.Types;
using Cocktails.Mapper.Common;
using Cocktails.Models.Cms.Requests;
using HotChocolate.AspNetCore;
using HotChocolate.Data;
using HotChocolate.Types.Pagination;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

var contentRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

var builder = WebApplication.CreateBuilder(new WebApplicationOptions { ContentRootPath = contentRootPath });

// Add services to the container.

var msSqlConnectionString = builder.Configuration.GetConnectionString("MsSqlConnection");

builder.Services.AddControllers();

builder.Services.AddDbContext<CocktailsContext>(
    options => options.UseSqlServer(msSqlConnectionString));

builder.Services.AddTransient(typeof(DbContext), typeof(CocktailsContext));

builder.Services
    .AddGraphQLServer()
    .AddSorting()
    .SetPagingOptions(new PagingOptions { IncludeTotalCount = true })
    .RegisterDbContext<CocktailsContext>(DbContextKind.Synchronized)
    .AddQueryType(d => d.Name("rootQuery"))
        .AddTypeExtension<CocktailQueryType>()
        .AddTypeExtension<CocktailCategoryQueryType>()
        .AddTypeExtension<FlavorQueryType>()
        .AddTypeExtension<IngredientQueryType>()
        .AddTypeExtension<MeasureUnitQueryType>()
    .AddMutationType(d => d.Name("rootMutation"))
        .AddTypeExtension<CocktailMutationType>()
    .AllowIntrospection(builder.Environment.EnvironmentName == "Development");

builder.Services.AddAutoMapper<CmsMapperConfiguration>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CocktailGetManyQueryHandler>());
builder.Services.AddTransient<IRequestHandler<GetByIdQuery<long, Cocktail>, Cocktail>, GetByIdQueryHandler<long, Cocktail>>();
builder.Services.AddTransient<IRequestHandler<GetByIdQuery<long, CocktailCategory>, CocktailCategory>, GetByIdQueryHandler<long, CocktailCategory>>();
builder.Services.AddTransient<IRequestHandler<GetByIdQuery<long, Flavor>, Flavor>, GetByIdQueryHandler<long, Flavor>>();
builder.Services.AddTransient<IRequestHandler<GetByIdQuery<long, Ingredient>, Ingredient>, GetByIdQueryHandler<long, Ingredient>>();
builder.Services.AddTransient<IRequestHandler<GetByIdQuery<long, MeasureUnit>, MeasureUnit>, GetByIdQueryHandler<long, MeasureUnit>>();

var apiInfo = new ApiInfo { Name = "Cocktails CMS Api", Author = "Alex Utiansky" };
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
