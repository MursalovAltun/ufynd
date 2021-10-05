using System.Linq;
using Application.Components.HotelsScraper.Abstractions;
using Application.Components.HotelsScraper.Models;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using HtmlAgilityPack;

namespace Application.Components.HotelsScraper.Services.Scrapers
{
    [As(typeof(IHotelScraper))]
    public class HotelAlternativesScraper : IHotelScraper
    {
        public string Key => nameof(HotelScrap.AlternativeHotels);

        private const string BaseSelector = "//table[@id='althotelsTable']//td";

        private const string AltHotelNameSelector =
            BaseSelector + "//p[contains(@class, 'althotels-name')]//a[contains(@class, 'althotel_link')]";

        private const string AltHotelDescriptionSelector =
            BaseSelector + "//span[contains(@class, 'hp_compset_description')]";

        public object Extract(string htmlContent)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(htmlContent);

            return htmlDocument.DocumentNode.SelectNodes(BaseSelector).Select(alternativeHotel =>
                new AlternativeHotelInfoScrap
                {
                    HotelName = alternativeHotel.SelectSingleNode(AltHotelNameSelector).InnerText.Trim(),
                    Description = alternativeHotel.SelectSingleNode(AltHotelDescriptionSelector).InnerText.Trim(),
                    DetailsUrl =
                        alternativeHotel.SelectSingleNode(AltHotelNameSelector).GetAttributeValue("href", null),
                });
        }
    }
}