using System.Threading.Tasks;

namespace Application.Components.HotelRatesReports.Abstractions
{
    public interface IHotelRatesExcelReportEmailNotificationService
    {
        Task SendAsync(byte[] report);
    }
}