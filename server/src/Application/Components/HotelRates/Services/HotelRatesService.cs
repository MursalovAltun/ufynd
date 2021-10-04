using System;
using System.Collections.Generic;
using System.Linq;
using Application.Components.HotelRates.Abstractions;
using Application.Components.HotelRates.DTO;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;

namespace Application.Components.HotelRates.Services
{
    [As(typeof(IHotelRatesService))]
    public class HotelRatesService : IHotelRatesService
    {
        public IEnumerable<HotelWithRatesDto> GetHotelRates(
            IReadOnlyCollection<HotelWithRatesDto> hotelWithRates, int? hotelId, DateTime? arrivalDate)
        {
            if (hotelId.HasValue)
            {
                hotelWithRates = hotelWithRates
                    .Where(hotelWithRate => hotelWithRate.Hotel.HotelId == hotelId.Value)
                    .ToList()
                    .AsReadOnly();
            }

            if (arrivalDate.HasValue)
            {
                foreach (var hotelWithRate in hotelWithRates)
                {
                    hotelWithRate.HotelRates = hotelWithRate.HotelRates
                        .Where(hotelRate => hotelRate.TargetDay.Date == arrivalDate.Value.Date);
                }
            }

            return hotelWithRates;
        }
    }
}