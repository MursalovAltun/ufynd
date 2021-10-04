using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Components.HotelRates.Abstractions;
using Application.Components.HotelRates.DTO;
using Application.Components.HotelRates.Requests;
using Autofac.Extras.Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Moq;
using WebApi.Controllers;
using Xunit;

namespace WebApi.UnitTests.Controllers
{
    public class HotelRateControllerTests
    {
        private readonly HotelRateController _controller;
        private readonly AutoMock _mock;
        
        public HotelRateControllerTests()
        {
            _mock = AutoMock.GetLoose();
            _controller = _mock.Create<HotelRateController>();
        }

        [Fact]
        public async Task Should_Return_Hotel_Rates()
        {
            const string path = "path";
            
            var request = new HotelRatesFilterRequest
            {
                ArrivalDate = null,
                HotelId = null,
            };

            _mock.Mock<IWebHostEnvironment>()
                .SetupGet(environment => environment.WebRootPath)
                .Returns(path);

            _mock.Mock<IHotelRatesProvider>()
                .Setup(provider => provider.GetAsync(path))
                .ReturnsAsync(new List<HotelWithRatesDto>());

            var actual = await _controller.GetList(request);

            actual.Should().BeEquivalentTo(new List<HotelWithRatesDto>());
        }
    }
}