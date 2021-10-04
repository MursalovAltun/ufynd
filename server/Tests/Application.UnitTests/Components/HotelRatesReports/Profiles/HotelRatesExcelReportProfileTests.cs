using System;
using System.Collections.Generic;
using Application.Components.HotelRates.DTO;
using Application.Components.HotelRatesReports.Models;
using Application.Components.HotelRatesReports.Profiles;
using Application.UnitTests.Fixtures;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Components.HotelRatesReports.Profiles
{
    public class HotelRatesExcelReportProfileTests
    {
        private readonly IMapper _mapper;

        public HotelRatesExcelReportProfileTests()
        {
            var fixture = new TestFixture(typeof(HotelRatesExcelReportProfile));
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void Should_Map_HotelRate_To_HotelRatesExcelReport()
        {
            const int los = 3;
            const string currency = "EUR";
            const int adults = 2;
            const string rateId = "rateId";
            const string rateDescription = "rateDescription";
            const string rateName = "rateName";
            const decimal priceFloat = 456.22M;
            const int priceInteger = 45622;
            const string breakfastTag = "breakfast";
            const bool breakfastTagShape = true;
            const string breakfastIncluded = "Yes";

            var arrivalDate = DateTime.Now;
            var departureDate = arrivalDate.AddDays(los);
            
            var hotelRate = new HotelRateDto
            {
                TargetDay = arrivalDate,
                Adults = adults,
                Los = los,
                RateDescription = rateDescription,
                RateId = rateId,
                RateName = rateName,
                Price = new HotelPriceDto
                {
                    Currency = currency,
                    NumericFloat = priceFloat,
                    NumericInteger = priceInteger
                },
                RateTags = new List<HotelRateTagDto>
                {
                    new()
                    {
                        Name = breakfastTag,
                        Shape = breakfastTagShape
                    }
                }
            };

            var expected = new HotelRatesExcelReport
            {
                Adults = adults,
                Price = priceFloat,
                ArrivalDate = arrivalDate.Date,
                BreakfastIncluded = breakfastIncluded,
                DepartureDate = departureDate.Date,
                PriceCurrency = currency,
                RateName = rateName
            };

            var actual = _mapper.Map<HotelRatesExcelReport>(hotelRate);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}