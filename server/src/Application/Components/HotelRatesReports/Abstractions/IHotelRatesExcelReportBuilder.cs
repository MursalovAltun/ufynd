using System.Collections.Generic;
using Application.Components.HotelRates.DTO;

namespace Application.Components.HotelRatesReports.Abstractions
{
    public interface IHotelRatesExcelReportBuilder
    {
        byte[] Build(IReadOnlyCollection<HotelWithRatesDto> hotelWithRates);
    }
}