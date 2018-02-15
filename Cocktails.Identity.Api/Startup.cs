using System;
using System.IO;

using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

using Cocktails.Data.Domain;
using Cocktails.Data.EFCore.Contexts;
using Cocktails.Common.Models;
using Cocktails.Identity.Mapper;
using Cocktails.Identity.Services;
using Cocktails.Mapper;
using Cocktails.Data.Identity;
using Cocktails.Data.EFCore.Repositories;
using Cocktails.Mailing;
using Cocktails.Mailing.Mailgun;

namespace Cocktails.Identity.Api
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
            services.AddMvc();

            services.AddApiVersioning();

            services.AddAutoMapper(expr => expr.AddProfile(typeof(MappingProfile)));

            services.Configure<AuthSettings>(Configuration.GetSection("AuthSettings"));
            services.Configure<MailingSettings>(Configuration.GetSection("MailingSettings"));
            services.Configure<MailgunSettings>(Configuration.GetSection("MailgunSettings"));
            services.Configure<ApiInfo>(Configuration.GetSection("IdentityApiInfo"));

            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders();

            //services.AddAuthentication(x => x.AddScheme())

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;

            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Identity API",
                    Description = "A simple example ASP.NET Core 2 Web API",
                    TermsOfService = "None"
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Cocktails.Identity.Api.xml");
                s.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(DbContext), typeof(IdentityContext));

            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(IModelMapper), typeof(ModelMapper));
            services.AddScoped(typeof(IUserStore<User>), typeof(IdentityUserStore));
            services.AddScoped(typeof(IRoleStore<Role>), typeof(IdentityRoleStore));
            services.AddScoped(typeof(IUserStorage<long>), typeof(UserStorage));
            services.AddScoped(typeof(IRoleStorage<long>), typeof(RoleStorage));
            services.AddScoped(typeof(ILoginStorage), typeof(LoginStorage));
            services.AddScoped(typeof(IMailgunApiClient), typeof(MailgunApiClient));
            services.AddScoped(typeof(IMailSender), typeof(MailgunSender));
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

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cocktails API v1");
            });
        }
    }
}
