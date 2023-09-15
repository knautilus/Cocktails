using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Cocktails.Mapper.Common
{
    public static class AutoMapperRegistrationExtensions
    {
        public static IServiceCollection AddAutoMapper<T>(
            this IServiceCollection services) where T : IMapperProfileConfiguration
        {
            services.AddSingleton(typeof(T));
            services.AddSingleton<IConfigurationProvider>(sp => new MapperConfiguration(cfg =>
            {
                var profileConfiguration = sp.GetService<T>();
                cfg.AddProfiles(profileConfiguration.GetProfiles());
            }));
            services.Add(new ServiceDescriptor(typeof(IMapper),
                sp => new AutoMapper.Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService),
                ServiceLifetime.Transient));

            return services;
        }
    }
}
