using Application.Components.HotelsScraper.Abstractions;
using Application.Components.HotelsScraper.Models;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using Newtonsoft.Json;

namespace Application.Components.HotelsScraper.Services
{
    [As(typeof(IHotelScraperService))]
    public class HotelScraperService : IHotelScraperService
    {
        private readonly IHotelScrapFactory _factory;

        public HotelScraperService(IHotelScrapFactory factory)
        {
            _factory = factory;
        }

        public HotelScrap ScrapHotelFromHtml(string htmlContent)
        {
            var scrappedObject = _factory.Create(htmlContent);

            var json = JsonConvert.SerializeObject(scrappedObject);

            return JsonConvert.DeserializeObject<HotelScrap>(json);
        }
    }
}