using Application.Components.HotelsScraper.Models;

namespace Application.Components.HotelsScraper.Abstractions
{
    public interface IHotelScraperService
    {
        HotelScrap ScrapHotelFromHtml(string htmlContent);
    }
}