using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Cocktails.Common.Models;
using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Contexts;
using Cocktails.Data.EntityFramework.Options;
using Cocktails.Data.EntityFramework.Repositories;
using Cocktails.Services;

namespace Cocktails.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

            services.AddScoped(typeof(IService<Cocktail>), typeof(CocktailService));
            services.AddScoped(typeof(IService<Flavor>), typeof(FlavorService));
            services.AddScoped(typeof(IService<Ingredient>), typeof(IngredientService));
            services.AddScoped(typeof(IService<Category>), typeof(CategoryService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var options = new RewriteOptions()
               .AddRedirectToHttps();

            app.UseRewriter(options);

            app.UseMvc();
        }
    }
}
