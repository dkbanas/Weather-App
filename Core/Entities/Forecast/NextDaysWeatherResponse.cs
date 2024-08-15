using System.Text.Json.Serialization;

namespace Core.Entities;

public class NextDaysWeatherResponse
{
    [JsonPropertyName("cod")]
    public string Cod { get; set; }

    [JsonPropertyName("message")]
    public int Message { get; set; }

    [JsonPropertyName("cnt")]
    public int Count { get; set; }

    [JsonPropertyName("list")]
    public List<NextDaysWeatherInfo> WeatherInfoList { get; set; }

    [JsonPropertyName("city")]
    public NextDaysCityInfo City { get; set; }
}