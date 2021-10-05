using System;
using System.IO;
using Application.Components.WebAssets;
using Application.Components.WebAssets.Abstractions;
using Application.Components.WebAssets.Services;
using Autofac.Extras.Moq;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;

namespace Application.UnitTests.WebAssets.Services
{
    public class WebAssetsPathProviderTests
    {
        [Fact]
        public void Should_Return_Asset_Relative_Path()
        {
            const string assetsDir = "AssetsDir";
            const string assetName = "test.json";
            
            var mock = AutoMock.GetLoose();

            mock.Mock<IOptions<WebAssetsOptions>>()
                .SetupGet(options => options.Value)
                .Returns(new WebAssetsOptions
                {
                    DirectoryName = assetsDir
                });

            mock.Create<WebAssetsPathProvider>()
                .Get(assetName)
                .Should()
                .BeEquivalentTo($"{assetsDir}{Path.DirectorySeparatorChar}{assetName}");
        }
    }
}