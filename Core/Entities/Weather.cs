using System.Text.Json.Serialization;

namespace Core.Entities;

public class Weather
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("main")]
    public string Main { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("icon")]
    public string Icon { get; set; }
}