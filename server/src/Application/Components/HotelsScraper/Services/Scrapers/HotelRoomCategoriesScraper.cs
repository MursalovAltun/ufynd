using System.Linq;
using Application.Components.HotelsScraper.Abstractions;
using Application.Components.HotelsScraper.Models;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace Application.Components.HotelsScraper.Services.Scrapers
{
    [As(typeof(IHotelScraper))]
    public class HotelRoomCategoriesScraper : IHotelScraper
    {
        public string Key => nameof(HotelScrap.RoomCategories);

        private const string Selector = "//table[@id='maxotel_rooms']//tr//td[contains(@class, 'ftd')]";

        public object Extract(string htmlContent)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(htmlContent);

            return htmlDocument.DocumentNode
                .SelectNodes(Selector)
                .Select(roomCategory => roomCategory.InnerText.Trim());
        }
    }
}