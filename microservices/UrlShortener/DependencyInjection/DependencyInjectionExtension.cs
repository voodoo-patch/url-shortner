using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using KeysDAL.Configuration;
using KeysDAL.Repository;
using UrlShortener.Services;

namespace UrlShortener.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            services.AddScoped<IShortenerService, ShortenerService>();
            services.AddScoped<IKeyGeneratorService, KeyGeneratorService>();

            return services;
        }
    }
}