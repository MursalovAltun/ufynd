using System.ComponentModel.DataAnnotations;

namespace Application.Components.WebAssets
{
    public class WebAssetsOptions
    {
        [Required] public string DirectoryName { get; set; }
        [Required] public string HotelRatesAssetName { get; set; }
    }
}