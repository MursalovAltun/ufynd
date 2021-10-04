namespace Application.Components.WebAssets.Abstractions
{
    public interface IWebAssetsPathProvider
    {
        string Get(string assetName);
    }
}