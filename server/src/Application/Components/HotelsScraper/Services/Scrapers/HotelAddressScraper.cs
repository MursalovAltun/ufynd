using Application.Components.HotelsScraper.Abstractions;
using Application.Components.HotelsScraper.Models;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using HtmlAgilityPack;

namespace Application.Components.HotelsScraper.Services.Scrapers
{
    [As(typeof(IHotelScraper))]
    public class HotelAddressScraper : IHotelScraper
    {
        public string Key => nameof(HotelScrap.Address);
        
        private const string HotelAddressId = "hp_address_subtitle";

        public object Extract(string htmlContent)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(htmlContent);

            return htmlDocument.GetElementbyId(HotelAddressId).InnerText.Trim();
        }
    }
}