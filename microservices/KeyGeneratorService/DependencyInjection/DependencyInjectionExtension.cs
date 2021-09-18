using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KeyGeneratorService.Services;

namespace KeyGeneratorService.DependencyInjection
{    
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(
             this IServiceCollection services)
        {
            services.AddHostedService<KeygenService>();

            return services;
        }

        public static IServiceCollection AddDatasource(
             this IServiceCollection services, IConfiguration config)
        {

            return services;
        }
    }
}