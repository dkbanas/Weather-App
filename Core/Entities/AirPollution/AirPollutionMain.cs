using System.Text.Json.Serialization;

namespace Core.Entities.AirPollution;

public class AirPollutionMain
{
    [JsonPropertyName("aqi")]
    public int Aqi { get; set; }
}