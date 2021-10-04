using System;

namespace Application.Components.HotelRatesReports.Models
{
    public class HotelRatesExcelReport
    {
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public decimal Price { get; set; }
        public string PriceCurrency { get; set; }
        public string RateName { get; set; }
        public int Adults { get; set; }
        public string BreakfastIncluded { get; set; }
    }
}