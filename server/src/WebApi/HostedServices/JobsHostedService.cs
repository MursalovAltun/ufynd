using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Components.HotelRates.Abstractions;
using Application.Components.HotelRatesReports.Abstractions;
using Application.Components.HotelRatesReports.Options;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace WebApi.HostedServices
{
    public class JobsHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWebHostEnvironment _environment;
        private readonly HotelRatesReportJobOptions _hotelRatesReportJobOptions;

        public JobsHostedService(IServiceProvider serviceProvider,
            IWebHostEnvironment environment,
            IOptions<HotelRatesReportJobOptions> hotelRatesReportJobOptions)
        {
            _serviceProvider = serviceProvider;
            _environment = environment;
            _hotelRatesReportJobOptions = hotelRatesReportJobOptions.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var hotelRatesProvider = scope.ServiceProvider.GetService<IHotelRatesProvider>();

            var hotelRates = await hotelRatesProvider!.GetAsync(_environment.WebRootPath);

            RecurringJob.AddOrUpdate<IHotelRatesReportJob>(_hotelRatesReportJobOptions.JobId, 
                x => x.SendReportEmailsAsync(hotelRates),
                Cron.Minutely);

            RecurringJob.Trigger(_hotelRatesReportJobOptions.JobId);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}