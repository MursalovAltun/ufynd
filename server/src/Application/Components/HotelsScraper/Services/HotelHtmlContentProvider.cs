using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Application.Components.HotelsScraper.Abstractions;
using Application.Components.WebAssets;
using Application.Components.WebAssets.Abstractions;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using Microsoft.Extensions.Options;

namespace Application.Components.HotelsScraper.Services
{
    [As(typeof(IHotelHtmlContentProvider))]
    public class HotelHtmlContentProvider : IHotelHtmlContentProvider
    {
        private readonly IWebAssetsPathProvider _webAssetsPathProvider;
        private readonly IFileSystem _fileSystem;
        private readonly WebAssetsOptions _options;

        public HotelHtmlContentProvider(IWebAssetsPathProvider webAssetsPathProvider,
            IOptions<WebAssetsOptions> options,
            IFileSystem fileSystem)
        {
            _webAssetsPathProvider = webAssetsPathProvider;
            _fileSystem = fileSystem;
            _options = options.Value;
        }
        
        public async Task<string> FromAsset(string assetsPath)
        {
            var htmlPath = Path.Combine(assetsPath, _webAssetsPathProvider.Get(_options.HotelHtmlAssetName));

            return await _fileSystem.File.ReadAllTextAsync(htmlPath);
        }

        public Task<string> FromWebPage(string url)
        {
            throw new System.NotImplementedException();
        }
    }
}