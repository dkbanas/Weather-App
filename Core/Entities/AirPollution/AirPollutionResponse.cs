using System.Text.Json.Serialization;

namespace Core.Entities.AirPollution;

public class AirPollutionResponse
{
    [JsonPropertyName("coord")]
    public Coord Coord { get; set; }
    
    [JsonPropertyName("list")]
    public List<AirPollutionData> List { get; set; }
}