using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Components.HotelRates.Abstractions;
using Application.Components.HotelRates.DTO;
using Application.Components.HotelRates.Requests;
using Application.Components.HotelRatesReports.Abstractions;
using Application.Components.HotelRatesReports.Options;
using Autofac.Extras.Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using WebApi.Controllers;
using Xunit;

namespace WebApi.UnitTests.Controllers
{
    public class HotelRateControllerTests
    {
        private readonly HotelRateReportController _controller;
        private readonly AutoMock _mock;

        private const string reportContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string reportFileName = "testfilename";

        public HotelRateControllerTests()
        {
            _mock = AutoMock.GetLoose();

            _mock.Mock<IOptions<HotelRatesReportJobOptions>>()
                .SetupGet(options => options.Value)
                .Returns(new HotelRatesReportJobOptions {ReportFileName = reportFileName});

            _controller = _mock.Create<HotelRateReportController>();
        }

        [Fact]
        public async Task Should_Return_Hotel_Rates_Report()
        {
            // In fact controllers must be integration tested but here we can verify filename and content type set correctly
            var report = new byte[] {1, 23, 25};

            _mock.Mock<IHotelRatesExcelReportBuilder>()
                .Setup(builder => builder.Build(null))
                .Returns(report);

            var actual = await _controller.Get();

            actual.Should().BeEquivalentTo(new FileContentResult(report, reportContentType)
            {
                FileDownloadName = reportFileName
            });
        }
    }
}