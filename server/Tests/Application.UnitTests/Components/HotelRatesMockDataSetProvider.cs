using System;
using System.Collections.Generic;
using Application.Components.HotelRates.DTO;

namespace Application.UnitTests.Components
{
    public class HotelRatesMockDataSetProvider
    {
        public readonly int Hotel1Id = 1;
        public readonly int Hotel2Id = 2;
        public readonly string Hotel1RateName = "Hotel1RateName";
        public readonly string Hotel2RateName = "Hotel2RateName";
        public DateTime Hotel1ArrivalDate = DateTime.Today;
        public DateTime Hotel2ArrivalDate = DateTime.Today.AddDays(3);

        public List<HotelWithRatesDto> Get()
        {
            return new()
            {
                new HotelWithRatesDto
                {
                    Hotel = new HotelDto
                    {
                        HotelId = Hotel1Id
                    },
                    HotelRates = new List<HotelRateDto>
                    {
                        new()
                        {
                            RateName = Hotel1RateName,
                            TargetDay = Hotel1ArrivalDate
                        }
                    }
                },
                new HotelWithRatesDto
                {
                    Hotel = new HotelDto
                    {
                        HotelId = Hotel2Id
                    },
                    HotelRates = new List<HotelRateDto>
                    {
                        new()
                        {
                            RateName = Hotel2RateName,
                            TargetDay = Hotel2ArrivalDate
                        }
                    }
                }
            };
        }
    }
}