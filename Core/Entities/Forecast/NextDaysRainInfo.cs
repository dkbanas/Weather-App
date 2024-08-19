using System.Text.Json.Serialization;

namespace Core.Entities.Forecast;

public class NextDaysRainInfo
{
    [JsonPropertyName("3h")]
    public double VolumeLast3Hours { get; set; }
}