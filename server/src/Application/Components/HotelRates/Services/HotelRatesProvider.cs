using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Components.HotelRates.Abstractions;
using Application.Components.HotelRates.DTO;
using Application.Components.WebAssets;
using Application.Components.WebAssets.Abstractions;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Application.Components.HotelRates.Services
{
    [As(typeof(IHotelRatesProvider))]
    public class HotelRatesProvider : IHotelRatesProvider
    {
        private readonly IWebAssetsPathProvider _webAssetsPathProvider;
        private readonly IFileSystem _fileSystem;
        private readonly WebAssetsOptions _webAssetsOptions;

        public HotelRatesProvider(IWebAssetsPathProvider webAssetsPathProvider,
            IFileSystem fileSystem,
            IOptions<WebAssetsOptions> webAssetsOptions)
        {
            _webAssetsPathProvider = webAssetsPathProvider;
            _fileSystem = fileSystem;
            _webAssetsOptions = webAssetsOptions.Value;
        }

        public async Task<IReadOnlyCollection<HotelWithRatesDto>> GetAsync(string path)
        {
            var hotelsRateJsonAssetPath =
                Path.Combine(path, _webAssetsPathProvider.Get(_webAssetsOptions.HotelRatesAssetName));

            var hotelsRatesJson = await _fileSystem.File.ReadAllTextAsync(hotelsRateJsonAssetPath, Encoding.UTF8);

            return JsonConvert.DeserializeObject<IReadOnlyCollection<HotelWithRatesDto>>(hotelsRatesJson);
        }
    }
}