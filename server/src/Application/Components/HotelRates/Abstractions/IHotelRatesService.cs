using System;
using System.Collections.Generic;
using Application.Components.HotelRates.DTO;

namespace Application.Components.HotelRates.Abstractions
{
    public interface IHotelRatesService
    {
        IEnumerable<HotelWithRatesDto> GetHotelRates(IReadOnlyCollection<HotelWithRatesDto> hotelWithRates,
            int? hotelId, DateTime? arrivalDate);
    }
}