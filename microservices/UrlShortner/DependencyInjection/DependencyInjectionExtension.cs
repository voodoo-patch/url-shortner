using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortner.Repository;
using UrlShortner.Services;

namespace UrlShortner.DependencyInjection
{    
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(
             this IServiceCollection services)
        {
            services.AddScoped<IShortnerService, ShortnerService>();
            services.AddScoped<IShortnerRepository, ShortnerRepository>();
            services.AddScoped<IKeyGeneratorService, KeyGeneratorService>();

            return services;
        }

        public static IServiceCollection AddDatasource(
             this IServiceCollection services, IConfiguration config)
        {

            return services;
        }
    }
}