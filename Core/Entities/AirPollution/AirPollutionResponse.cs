using System.Text.Json.Serialization;

namespace Core.Entities;

public class AirPollutionResponse
{
    [JsonPropertyName("coord")]
    public Coord Coord { get; set; }
    
    [JsonPropertyName("list")]
    public List<AirPollutionData> List { get; set; }
}