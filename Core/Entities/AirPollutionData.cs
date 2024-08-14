using System.Text.Json.Serialization;

namespace Core.Entities;

public class AirPollutionData
{
    [JsonPropertyName("dt")]
    public int Dt { get; set; }
    
    [JsonPropertyName("main")]
    public AirPollutionMain Main { get; set; }
    
    [JsonPropertyName("components")]
    public Components Components { get; set; }
}