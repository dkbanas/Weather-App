using System.Text.Json.Serialization;

namespace Core.Entities.CurrentWeather;

public class Clouds
{
    [JsonPropertyName("all")]
    public int All { get; set; }
}