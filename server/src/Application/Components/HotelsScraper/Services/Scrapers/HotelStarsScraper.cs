using System.Linq;
using Application.Components.HotelsScraper.Abstractions;
using Application.Components.HotelsScraper.Models;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using HtmlAgilityPack;

namespace Application.Components.HotelsScraper.Services.Scrapers
{
    [As(typeof(IHotelScraper))]
    public class HotelStarsScraper : IHotelScraper
    {
        public string Key => nameof(HotelScrap.Stars);

        private const string Selector = "//span[contains(@class, 'hp__hotel_ratings__stars')]//i";

        public object Extract(string htmlContent)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(htmlContent);

            return htmlDocument.DocumentNode
                .SelectSingleNode(Selector)
                .GetClasses()
                .First(x => x.Contains("ratings_stars"))
                .Last()
                .ToString();
        }
    }
}