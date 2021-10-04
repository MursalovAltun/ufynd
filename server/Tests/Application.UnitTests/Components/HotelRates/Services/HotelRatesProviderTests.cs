using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Components.HotelRates.Abstractions;
using Application.Components.HotelRates.Services;
using Application.Components.WebAssets;
using Application.Components.WebAssets.Abstractions;
using Autofac.Extras.Moq;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Application.UnitTests.Components.HotelRates.Services
{
    public class HotelRatesProviderTests
    {
        [Fact]
        public async Task Should_Return_Hotel_Rates()
        {
            const string root = "C:";
            const string assetName = "test.json";
            const string assetsFolderName = "Assets";

            using var mock = AutoMock.GetLoose();
            
            mock.Mock<IOptions<WebAssetsOptions>>()
                .SetupGet(options => options.Value)
                .Returns(new WebAssetsOptions
                {
                    HotelRatesAssetName = assetName
                });
            
            var service = mock.Create<HotelRatesProvider>();

            mock.Mock<IFileSystem>()
                .Setup(fileSystem => fileSystem.File.ReadAllTextAsync(Path.Combine(root, assetsFolderName, assetName),
                    Encoding.Default, CancellationToken.None))
                .ReturnsAsync("test");

            mock.Mock<IWebAssetsPathProvider>()
                .Setup(provider => provider.Get(assetName))
                .Returns($@"{assetsFolderName}\{assetName}");
            
            await service.GetAsync(root);
        }
    }
}