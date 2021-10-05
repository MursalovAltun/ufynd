using Application.Components.HotelsScraper.Abstractions;
using Application.Components.HotelsScraper.Models;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using HtmlAgilityPack;

namespace Application.Components.HotelsScraper.Services.Scrapers
{
    [As(typeof(IHotelScraper))]
    public class HotelNumberOfReviewsScraper : IHotelScraper
    {
        public string Key => nameof(HotelScrap.NumberOfReviews);

        private const string Selector = "//*[@id='js--hp-gallery-scorecard']//span[contains(@class, 'score_from_number_of_reviews')]//strong[contains(@class, 'count')]";

        public object Extract(string htmlContent)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(htmlContent);

            return htmlDocument
                .DocumentNode
                .SelectSingleNode(Selector)
                .InnerText
                .Trim();
        }
    }
}