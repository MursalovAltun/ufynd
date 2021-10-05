using System.Dynamic;

namespace Application.Components.HotelsScraper.Abstractions
{
    public interface IHotelScrapFactory
    {
        ExpandoObject Create(string htmlContent);
    }
}