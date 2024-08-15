using System.Text.Json.Serialization;

namespace Core.Entities;

public class NextDaysRainInfo
{
    [JsonPropertyName("3h")]
    public double VolumeLast3Hours { get; set; }
}