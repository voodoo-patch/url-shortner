using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using UrlsDAL.Configuration;
using UrlsDAL.Repository;

namespace UrlsDAL.DependencyInjection
{    
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddUrlServices(
             this IServiceCollection services)
        {
            services.AddSingleton<IShortenerRepository, ShortenerRepository>();

            return services;
        }

        public static IServiceCollection AddUrlsDB(
             this IServiceCollection services, IConfiguration config)
        {
            // requires using Microsoft.Extensions.Options
            services.Configure<UrlDatabaseSettings>(
                config.GetSection(nameof(UrlDatabaseSettings)));
            services.AddSingleton<IUrlDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<UrlDatabaseSettings>>().Value);
            
            return services;
        }
    }
}