using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class CommonConfiguration
    {
        [Required] public string ClientBaseUrl { get; set; }
    }
}