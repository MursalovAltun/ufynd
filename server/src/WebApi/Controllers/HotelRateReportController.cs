using System.Threading.Tasks;
using Application.Components.HotelRates.Abstractions;
using Application.Components.HotelRatesReports.Abstractions;
using Application.Components.HotelRatesReports.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelRateReportController : ControllerBase
    {
        private readonly IHotelRatesExcelReportBuilder _hotelRatesExcelReportBuilder;
        private readonly IHotelRatesProvider _hotelRatesProvider;
        private readonly HotelRatesReportJobOptions _options;
        private readonly IWebHostEnvironment _environment;

        public HotelRateReportController(IHotelRatesExcelReportBuilder hotelRatesExcelReportBuilder,
            IHotelRatesProvider hotelRatesProvider,
            IOptions<HotelRatesReportJobOptions> options,
            IWebHostEnvironment environment)
        {
            _hotelRatesExcelReportBuilder = hotelRatesExcelReportBuilder;
            _hotelRatesProvider = hotelRatesProvider;
            _options = options.Value;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotelWithRates = await _hotelRatesProvider.GetAsync(_environment.WebRootPath);

            var report = _hotelRatesExcelReportBuilder.Build(hotelWithRates);

            return File(report, _options.ReportContentType, _options.ReportFileName);
        }
    }
}