using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Components.HotelRates.DTO;

namespace Application.Components.HotelRatesReports.Abstractions
{
    public interface IHotelRatesReportJob
    {
        Task SendReportEmailsAsync(IReadOnlyCollection<HotelWithRatesDto> hotelWithRates);
    }
}