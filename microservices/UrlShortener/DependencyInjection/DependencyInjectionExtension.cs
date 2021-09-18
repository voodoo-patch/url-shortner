using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Repository;
using UrlShortener.Services;

namespace UrlShortener.DependencyInjection
{    
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(
             this IServiceCollection services)
        {
            services.AddScoped<IShortenerService, ShortenerService>();
            services.AddScoped<IShortenerRepository, ShortenerRepository>();
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