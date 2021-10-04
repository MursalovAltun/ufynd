using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Filters;

namespace WebApi.Configurations
{
    public static class ControllersConfiguration
    {
        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.Filters.Add<BadRequestExceptionFilter>();
                })
                .AddFluentValidation()
                .AddNewtonsoftJson();

            return services;
        }

        public static IApplicationBuilder UseConfiguredControllers(this IApplicationBuilder app)
        {
            app.UseRouting();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            return app;
        }
    }
}