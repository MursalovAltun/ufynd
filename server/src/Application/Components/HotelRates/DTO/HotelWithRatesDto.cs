using System.Collections.Generic;

namespace Application.Components.HotelRates.DTO
{
    public class HotelWithRatesDto
    {
        public HotelDto Hotel { get; set; }
        public IEnumerable<HotelRateDto> HotelRates { get; set; }
    }
}