namespace Application.Components.HotelsScraper.Abstractions
{
    public interface IHotelScraper
    {
        string Key { get; }
        object Extract(string htmlContent);
    }
}