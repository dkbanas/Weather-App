using System.Text.Json.Serialization;

namespace Core.Entities.CurrentWeather;

public class Rain
{
    [JsonPropertyName("_1h")]
    public double? _1h { get; set; }
}