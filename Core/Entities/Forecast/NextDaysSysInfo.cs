using System.Text.Json.Serialization;

namespace Core.Entities.Forecast;

public class NextDaysSysInfo
{
    [JsonPropertyName("pod")]
    public string Pod { get; set; }
}