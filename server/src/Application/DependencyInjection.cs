using System.IO.Abstractions;
using System.Reflection;
using Application.Components.EmailSender;
using Application.Components.HotelRatesReports.Options;
using Application.Components.WebAssets;
using Application.OptionsValidation;
using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IFileSystem, FileSystem>();

            services.AddHttpContextAccessor();
            
            services.AddConfigurations(configuration);

            return services;
        }

        private static IServiceCollection AddConfigurations(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureAndValidate<CommonConfiguration>(configuration.GetSection("Common"));
            
            services.ConfigureAndValidate<WebAssetsOptions>(configuration.GetSection("WebAssets"));
            
            services.ConfigureAndValidate<HotelRatesReportJobOptions>(configuration.GetSection("HotelRatesReportJob"));
            
            var emailConfiguration = configuration.GetSection("EmailConfiguration");
            services.ConfigureAndValidate<EmailConfiguration>(emailConfiguration);

            if (emailConfiguration.Get<EmailConfiguration>()?.UseMailHog ?? false)
            {
                services.AddScoped<IEmailService, MailHogEmailService>();
            }
            else
            {
                services.AddScoped<IEmailService, SendGridEmailService>();
                services.AddScoped<ISendGridClientContainer, SendGridClientContainer>();
            }

            return services;
        }
    }
}