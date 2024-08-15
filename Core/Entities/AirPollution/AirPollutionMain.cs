using System.Text.Json.Serialization;

namespace Core.Entities;

public class AirPollutionMain
{
    [JsonPropertyName("aqi")]
    public int Aqi { get; set; }
}