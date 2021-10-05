using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Components.HotelRates.DTO;
using Application.Components.HotelRatesReports.Abstractions;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;

namespace Application.Components.HotelRatesReports.Jobs
{
    [As(typeof(IHotelRatesReportJob))]
    public class HotelRatesReportJob : IHotelRatesReportJob
    {
        private readonly IHotelRatesExcelReportBuilder _hotelRatesExcelReportBuilder;
        private readonly IHotelRatesExcelReportEmailNotificationService _hotelRatesExcelReportEmailNotificationService;

        public HotelRatesReportJob(IHotelRatesExcelReportBuilder hotelRatesExcelReportBuilder,
            IHotelRatesExcelReportEmailNotificationService hotelRatesExcelReportEmailNotificationService)
        {
            _hotelRatesExcelReportBuilder = hotelRatesExcelReportBuilder;
            _hotelRatesExcelReportEmailNotificationService = hotelRatesExcelReportEmailNotificationService;
        }

        public async Task SendReportEmailsAsync(IReadOnlyCollection<HotelWithRatesDto> hotelWithRates)
        {
            var report = _hotelRatesExcelReportBuilder.Build(hotelWithRates);

            await _hotelRatesExcelReportEmailNotificationService.SendAsync(report);
        }
    }
}