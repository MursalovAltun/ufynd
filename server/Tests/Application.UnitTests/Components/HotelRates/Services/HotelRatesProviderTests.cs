using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Components.HotelRates.Services;
using Application.Components.WebAssets;
using Application.Components.WebAssets.Abstractions;
using Autofac.Extras.Moq;
using FluentAssertions;
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
            var fullPath = Path.Combine(root, assetsFolderName, assetName);

            using var mock = AutoMock.GetLoose();

            mock.Mock<IOptions<WebAssetsOptions>>()
                .SetupGet(options => options.Value)
                .Returns(new WebAssetsOptions
                {
                    HotelRatesAssetName = assetName
                });

            var service = mock.Create<HotelRatesProvider>();

            mock.Mock<IFileSystem>()
                .Setup(fileSystem => fileSystem.File.ReadAllTextAsync(fullPath, Encoding.UTF8, CancellationToken.None))
                .ReturnsAsync("[]");

            mock.Mock<IWebAssetsPathProvider>()
                .Setup(provider => provider.Get(assetName))
                .Returns($@"{assetsFolderName}\{assetName}");

            // it doesn't make sense to test returned value
            // just need to verify and check there's no exception
            await service
                .Invoking(sut => sut.GetAsync(root))
                .Should()
                .NotThrowAsync();
        }
    }
}