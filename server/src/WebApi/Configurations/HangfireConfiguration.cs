using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi.Configurations
{
    public static class HangfireConfiguration
    {
        public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(options =>
                options.UsePostgreSqlStorage(configuration.GetConnectionString("DefaultConnection")));

            services.AddHangfireServer();

            return services;
        }

        public static IApplicationBuilder UseConfiguredHangfire(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment()) return app;
            
            app.UseHangfireDashboard();

            return app;
        }
    }
}