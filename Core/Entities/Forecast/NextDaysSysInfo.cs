using System.Text.Json.Serialization;

namespace Core.Entities;

public class NextDaysSysInfo
{
    [JsonPropertyName("pod")]
    public string Pod { get; set; }
}