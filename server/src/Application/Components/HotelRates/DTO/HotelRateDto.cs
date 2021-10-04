using System;
using System.Collections.Generic;

namespace Application.Components.HotelRates.DTO
{
    public class HotelRateDto
    {
        public int Adults { get; set; }
        /// <summary>
        /// Number of room nights
        /// </summary>
        public int Los { get; set; }
        public HotelPriceDto Price { get; set; }
        public string RateDescription { get; set; }
        public string RateId { get; set; }
        public string RateName { get; set; }
        public List<HotelRateTagDto> RateTags { get; set; }
        public DateTime TargetDay { get; set; }
    }
}