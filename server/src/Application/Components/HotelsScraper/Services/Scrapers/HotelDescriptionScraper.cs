using System.Linq;
using Application.Components.HotelsScraper.Abstractions;
using Application.Components.HotelsScraper.Models;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using HtmlAgilityPack;

namespace Application.Components.HotelsScraper.Services.Scrapers
{
    [As(typeof(IHotelScraper))]
    public class HotelDescriptionScraper : IHotelScraper
    {
        public string Key => nameof(HotelScrap.Description);

        private const string Selector = "//*[@id='summary']//p";

        public object Extract(string htmlContent)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(htmlContent);

            return string.Join("\n", htmlDocument
                .DocumentNode
                .SelectNodes(Selector)
                .Select(node => node.InnerText.Trim())
            );
        }
    }
}