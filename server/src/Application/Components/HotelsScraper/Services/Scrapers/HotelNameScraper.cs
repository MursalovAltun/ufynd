using Application.Components.HotelsScraper.Abstractions;
using Application.Components.HotelsScraper.Models;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using HtmlAgilityPack;

namespace Application.Components.HotelsScraper.Services.Scrapers
{
    [As(typeof(IHotelScraper))]
    public class HotelNameScraper : IHotelScraper
    {
        public string Key => nameof(HotelScrap.HotelName);

        private const string HotelNameId = "hp_hotel_name";
        
        public object Extract(string htmlContent)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(htmlContent);

            var nameNode = htmlDocument.GetElementbyId(HotelNameId);

            return nameNode.InnerText.Trim();
        }
    }
}