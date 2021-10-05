using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Components.HotelRates.Abstractions;
using Application.Components.HotelRates.DTO;
using Application.Components.HotelRates.Requests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelRateController : ControllerBase
    {
        private readonly IHotelRatesService _hotelRatesService;
        private readonly IHotelRatesProvider _hotelRatesProvider;
        private readonly IWebHostEnvironment _environment;

        public HotelRateController(IHotelRatesService hotelRatesService,
            IHotelRatesProvider hotelRatesProvider,
            IWebHostEnvironment environment)
        {
            _hotelRatesService = hotelRatesService;
            _hotelRatesProvider = hotelRatesProvider;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IEnumerable<HotelWithRatesDto>> GetList([FromQuery] HotelRatesFilterRequest request)
        {
            var hotelWithRates = await _hotelRatesProvider.GetAsync(_environment.WebRootPath);

            return _hotelRatesService.GetHotelRates(hotelWithRates, request.HotelId, request.ArrivalDate);
        }
    }
}