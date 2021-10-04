using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Components.HotelRates.DTO;

namespace Application.Components.HotelRates.Abstractions
{
    public interface IHotelRatesProvider
    {
        Task<IReadOnlyCollection<HotelWithRatesDto>> GetAsync(string path);
    }
}