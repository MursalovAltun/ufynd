using System.IO;
using Application.Components.WebAssets.Abstractions;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using Microsoft.Extensions.Options;

namespace Application.Components.WebAssets.Services
{
    [As(typeof(IWebAssetsPathProvider))]
    public class WebAssetsPathProvider : IWebAssetsPathProvider
    {
        private readonly WebAssetsOptions _webAssetsOptions;
        
        public WebAssetsPathProvider(IOptions<WebAssetsOptions> options)
        {
            _webAssetsOptions = options.Value;
        }

        public string Get(string assetName) => Path.Combine(_webAssetsOptions.DirectoryName, assetName);
    }
}