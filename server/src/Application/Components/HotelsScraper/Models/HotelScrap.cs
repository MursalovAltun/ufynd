using System;
using System.Collections.Generic;

namespace Application.Components.HotelsScraper.Models
{
    public class HotelScrap
    {

        public string HotelName { get; set; }
        public string Address { get; set; }
        public int Stars { get; set; }
        public double ReviewPoints { get; set; }
        public int NumberOfReviews { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> RoomCategories { get; set; }
        public IEnumerable<AlternativeHotelInfoScrap> AlternativeHotels { get; set; }
    }
}