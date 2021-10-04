using Application;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Configurations;
using WebApi.HostedServices;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private bool DOCKER_CONFIGURED => Configuration["DOCKER_CONFIGURED"] == "True";

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (DOCKER_CONFIGURED)
            {
                services.AddHangfire(options =>
                    options.UsePostgreSqlStorage(Configuration.GetConnectionString("DefaultConnection")));

                services.AddHangfireServer();

                services.AddHostedService<JobsHostedService>();
            }

            services.AddApplication(Configuration);

            services.ConfigureControllers();

            services.ConfigureCors();

            services.ConfigureSwagger();

            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                if (DOCKER_CONFIGURED)
                {
                    app.UseHangfireDashboard();
                }
            }

            app.UseConfiguredSwagger(env);

            app.UseConfiguredCors(env);

            app.UseConfiguredControllers();
        }
    }
}