using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using KeysDAL.Configuration;
using KeysDAL.Repository;

namespace KeysDAL.DependencyInjection
{    
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRepository(
             this IServiceCollection services)
        {
            services.AddSingleton<IKeyGeneratorRepository, KeyGeneratorRepository>();

            return services;
        }

        public static IServiceCollection AddKeysDB(
             this IServiceCollection services, IConfiguration config)
        {
            // requires using Microsoft.Extensions.Options
            
            services.Configure<KeyDatabaseSettings>(
                config.GetSection(nameof(KeyDatabaseSettings)));
            services.AddSingleton<IKeyDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<KeyDatabaseSettings>>().Value);

            return services;
        }
    }
}