using System.Threading.Tasks;
using Application.Components.HotelsScraper.Abstractions;
using Application.Components.HotelsScraper.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelScrapController : ControllerBase
    {
        private readonly IHotelHtmlContentProvider _hotelHtmlContentProvider;
        private readonly IWebHostEnvironment _environment;
        private readonly IHotelScraperService _hotelScraperService;

        public HotelScrapController(IHotelHtmlContentProvider hotelHtmlContentProvider,
            IWebHostEnvironment environment,
            IHotelScraperService hotelScraperService)
        {
            _hotelHtmlContentProvider = hotelHtmlContentProvider;
            _environment = environment;
            _hotelScraperService = hotelScraperService;
        }
        
        [HttpGet]
        public async Task<HotelScrap> Scrap()
        {
            var htmlContent = await _hotelHtmlContentProvider.FromAsset(_environment.WebRootPath);

            return _hotelScraperService.ScrapHotelFromHtml(htmlContent);
        }
    }
}