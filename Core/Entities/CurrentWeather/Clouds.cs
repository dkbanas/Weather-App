using System.Text.Json.Serialization;

namespace Core.Entities;

public class Clouds
{
    [JsonPropertyName("all")]
    public int All { get; set; }
}