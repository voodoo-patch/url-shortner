using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using UrlShortener.Configuration;
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
            services.AddSingleton<IShortenerRepository, ShortenerRepository>();
            services.AddScoped<IKeyGeneratorService, KeyGeneratorService>();
            services.AddSingleton<IKeyGeneratorRepository, KeyGeneratorRepository>();

            return services;
        }

        public static IServiceCollection AddDatasource(
             this IServiceCollection services, IConfiguration config)
        {
            // requires using Microsoft.Extensions.Options
            services.Configure<UrlDatabaseSettings>(
                config.GetSection(nameof(UrlDatabaseSettings)));
            services.AddSingleton<IUrlDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<UrlDatabaseSettings>>().Value);
            
            services.Configure<KeyDatabaseSettings>(
                config.GetSection(nameof(KeyDatabaseSettings)));
            services.AddSingleton<IKeyDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<KeyDatabaseSettings>>().Value);

            return services;
        }
    }
}