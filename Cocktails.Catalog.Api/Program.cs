﻿using Cocktails.Common.Models;
using Cocktails.Data.Contexts;
using Cocktails.GraphQL.Catalog.Types;
using HotChocolate.AspNetCore;
using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Builder;
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
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CocktailsContext>(
    options => options.UseSqlServer(msSqlConnectionString));

//builder.Services.AddTransient(typeof(CocktailsContext), typeof(CocktailsContext));

builder.Services
    .AddGraphQLServer()
    .AddSorting()
    .SetPagingOptions(new PagingOptions { IncludeTotalCount = true })
    .RegisterDbContext<CocktailsContext>()
    .AddQueryType(d => d.Name("rootQuery"))
        .AddTypeExtension<CategoryQueryType>()
        .AddTypeExtension<FlavorQueryType>()
        .AddTypeExtension<IngredientQueryType>()
        .AddTypeExtension<CocktailQueryType>()
    .AllowIntrospection(builder.Environment.EnvironmentName == "Development");

var apiInfo = new ApiInfo { Name = "Cocktails catalog api", Author = "Alex Utiansky" };
builder.Services.AddSingleton(typeof(ApiInfo), x => apiInfo);

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

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
