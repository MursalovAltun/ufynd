using System;
using System.Collections.Generic;
using Application.Components.HotelRates.Abstractions;
using Application.Components.HotelRates.DTO;
using Application.Components.HotelRates.Services;
using Autofac.Extras.Moq;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Components.HotelRates.Services
{
    public class HotelRatesServiceTests
    {
        private readonly IHotelRatesService _service;
        private readonly HotelRatesMockDataSetProvider _dataSetProvider;

        public HotelRatesServiceTests()
        {
            _dataSetProvider = new HotelRatesMockDataSetProvider();
            var mock = AutoMock.GetLoose();
            _service = mock.Create<HotelRatesService>();
        }

        [Fact]
        public void Should_Filter_Hotel_Rates_Based_On_HotelId_And_ArrivalDate()
        {
            const int hotelId = 1;
            var arrivalDate = DateTime.Today;

            var expected = new List<HotelWithRatesDto>
            {
                new()
                {
                    Hotel = new HotelDto
                    {
                        HotelId = _dataSetProvider.Hotel1Id,
                    },
                    HotelRates = new List<HotelRateDto>
                    {
                        new()
                        {
                            RateName = _dataSetProvider.Hotel1RateName,
                            TargetDay = _dataSetProvider.Hotel1ArrivalDate
                        }
                    }
                }
            };

            _service.GetHotelRates(_dataSetProvider.Get(), hotelId, arrivalDate)
                .Should()
                .BeEquivalentTo(expected);
        }

        [Fact]
        public void Should_Filter_Hotel_Rates_Based_On_HotelId()
        {
            const int hotelId = 2;

            var expected = new List<HotelWithRatesDto>
            {
                new()
                {
                    Hotel = new HotelDto
                    {
                        HotelId = _dataSetProvider.Hotel2Id,
                    },
                    HotelRates = new List<HotelRateDto>
                    {
                        new()
                        {
                            RateName = _dataSetProvider.Hotel2RateName,
                            TargetDay = _dataSetProvider.Hotel2ArrivalDate
                        }
                    }
                }
            };

            _service.GetHotelRates(_dataSetProvider.Get(), hotelId, null)
                .Should()
                .BeEquivalentTo(expected);
        }

        [Fact]
        public void Should_Filter_Hotel_Rates_Based_On_ArrivalDate()
        {
            var arrivalDate = DateTime.Today;

            var expected = new List<HotelWithRatesDto>
            {
                new()
                {
                    Hotel = new HotelDto
                    {
                        HotelId = _dataSetProvider.Hotel1Id,
                    },
                    HotelRates = new List<HotelRateDto>
                    {
                        new()
                        {
                            RateName = _dataSetProvider.Hotel1RateName,
                            TargetDay = _dataSetProvider.Hotel1ArrivalDate
                        }
                    }
                },
                new ()
                {
                    Hotel = new HotelDto
                    {
                        HotelId = _dataSetProvider.Hotel2Id
                    },
                    HotelRates = new List<HotelRateDto>()
                }
            };

            _service.GetHotelRates(_dataSetProvider.Get(), null, arrivalDate)
                .Should()
                .BeEquivalentTo(expected);
        }
    }
}