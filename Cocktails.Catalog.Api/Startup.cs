using System.IO;

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

using Cocktails.Catalog.Mapper;
using Cocktails.Catalog.Services;
using Cocktails.Catalog.Services.EFCore;
using Cocktails.Catalog.ViewModels;
using Cocktails.Common.Objects;
using Cocktails.Common.Services;
using Cocktails.Data;
using Cocktails.Data.Catalog;
using Cocktails.Data.Catalog.EFCore.Contexts;
using Cocktails.Data.EFCore.Repositories;
using Cocktails.Mapper;
using Cocktails.Security;

namespace Cocktails.Catalog.Api
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

            services.AddAuthentication(cfg => {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["AuthSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["AuthSettings:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = SecurityHelper.GetSymmetricSecurityKey(Configuration["AuthSettings:SecretKey"]),
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddApiVersioning();

            services.AddOptions();
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.Configure<ApiInfo>(Configuration.GetSection("CocktailsApiInfo"));

            services.AddAutoMapper(expr => expr.AddProfile(typeof(MappingProfile)));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Cocktails API",
                    Description = "A simple example ASP.NET Core 2 Web API",
                    TermsOfService = "None"
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Cocktails.Catalog.Api.xml");
                s.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<CocktailsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(DbContext), typeof(CocktailsContext));
            services.AddScoped(typeof(IContentRepository<,>), typeof(ContentRepository<,>));

            services.AddScoped(typeof(IService<Cocktail, CocktailModel>), typeof(CocktailService));
            services.AddScoped(typeof(IService<Flavor, FlavorModel>), typeof(FlavorService));
            services.AddScoped(typeof(IIngredientService), typeof(IngredientService));
            services.AddScoped(typeof(ICocktailService), typeof(CocktailService));
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

            var options = new RewriteOptions()
               .AddRedirectToHttps();

            app.UseRewriter(options);

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cocktails API v1");
            });
        }
    }
}
