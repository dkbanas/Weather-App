using System.Text.Json.Serialization;

namespace Core.Entities;

public class NextDaysWeatherInfo
{
    [JsonPropertyName("dt")]
    public long DateTime { get; set; }

    [JsonPropertyName("main")]
    public Main Main { get; set; }

    [JsonPropertyName("weather")]
    public List<NextDaysWeatherDescription> Weather { get; set; }

    [JsonPropertyName("clouds")]
    public Clouds Clouds { get; set; }

    [JsonPropertyName("wind")]
    public Wind Wind { get; set; }

    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }

    [JsonPropertyName("pop")]
    public double ProbabilityOfPrecipitation { get; set; }

    [JsonPropertyName("rain")]
    public NextDaysRainInfo Rain { get; set; }

    [JsonPropertyName("sys")]
    public NextDaysSysInfo Sys { get; set; }

    [JsonPropertyName("dt_txt")]
    public string DateTimeText { get; set; }
}