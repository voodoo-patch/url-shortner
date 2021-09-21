using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyGeneratorService.DependencyInjection;
using KeysDAL.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace KeyGeneratorService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfiguration configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KeyGeneratorService", Version = "v1" });
            });
            // services.AddLogging(configure => configure.AddConsole())
            //     .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug);
            services.AddLogging(builder =>
                builder
                    .AddDebug()
                    .AddConsole()
                    .AddConfiguration(configuration.GetSection("Logging"))
                    .SetMinimumLevel(LogLevel.Information)
            );
            services.AddServices();
            services.AddKeyServices();
            services.AddKeysDB(configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KeyGeneratorService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
