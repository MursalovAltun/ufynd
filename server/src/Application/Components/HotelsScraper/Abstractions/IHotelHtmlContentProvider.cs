using System.Threading.Tasks;

namespace Application.Components.HotelsScraper.Abstractions
{
    public interface IHotelHtmlContentProvider
    {
        Task<string> FromAsset(string assetsPath);
        Task<string> FromWebPage(string url);
    }
}