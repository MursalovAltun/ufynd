using System;

namespace Application.Components.HotelRates.Requests
{
    public class HotelRatesFilterRequest
    {
        public int? HotelId { get; set; }
        public DateTime? ArrivalDate { get; set; }
    }
}