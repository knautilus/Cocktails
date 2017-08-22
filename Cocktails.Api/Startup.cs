using System.IO;

using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

using Cocktails.Common.Models;
using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Contexts;
using Cocktails.Data.EntityFramework.Options;
using Cocktails.Data.EntityFramework.Repositories;
using Cocktails.Services;
using Cocktails.ViewModels;
using Cocktails.Mapper;

namespace Cocktails.Api
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddOptions();

            // Add settings from configuration
            services.Configure<ApiInfo>(Configuration.GetSection("ApiInfo"));

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddAutoMapper();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Cocktails API",
                    Description = "A simple example ASP.NET Core 2 Web API",
                    TermsOfService = "None"
                });

                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Cocktails.Api.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<CocktailsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(DbContext), typeof(CocktailsContext));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepositoryOptions), typeof(RepositoryOptions));

            services.AddScoped<IRepository<Mix>>(provider =>
            {
                return new Repository<Mix>(
                    provider.GetService<DbContext>(),
                    new RepositoryOptions { AutoCommit = false });
            });

            services.AddScoped(typeof(IService<Cocktail, CocktailModel>), typeof(CocktailService));
            services.AddScoped(typeof(IService<Flavor, FlavorModel>), typeof(FlavorService));
            services.AddScoped(typeof(IService<Ingredient, IngredientModel>), typeof(IngredientService));
            services.AddScoped(typeof(IService<Category, CategoryModel>), typeof(CategoryService));
            services.AddScoped(typeof(IModelMapper), typeof(ModelMapper));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var options = new RewriteOptions()
               .AddRedirectToHttps();

            app.UseRewriter(options);

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cocktails API v1");
            });
        }
    }
}
