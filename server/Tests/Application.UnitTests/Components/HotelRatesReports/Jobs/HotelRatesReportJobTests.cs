using System.Threading.Tasks;
using Application.Components.HotelRatesReports.Abstractions;
using Application.Components.HotelRatesReports.Jobs;
using Autofac.Extras.Moq;
using Moq;
using Xunit;

namespace Application.UnitTests.Components.HotelRatesReports.Jobs
{
    public class HotelRatesReportJobTests
    {
        [Fact]
        public async Task Should_Send_Report_Emails()
        {
            var mockHotelRatesProvider = new HotelRatesMockDataSetProvider();
            var mockHotelRates = mockHotelRatesProvider.Get();
            var mockReport = new byte[] {2, 15, 25};
            
            var mock = AutoMock.GetLoose();

            mock.Mock<IHotelRatesExcelReportBuilder>()
                .Setup(builder => builder.Build(mockHotelRates))
                .Returns(mockReport);

            var job = mock.Create<HotelRatesReportJob>();

            await job.SendReportEmailsAsync(mockHotelRates);

            mock.Mock<IHotelRatesExcelReportEmailNotificationService>()
                .Verify(builder => builder.SendAsync(mockReport), Times.Once);
        }
    }
}