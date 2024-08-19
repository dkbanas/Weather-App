using System.Globalization;
using System.Text.Json;
using Core.Entities;
using Core.Entities.AirPollution;
using Core.Entities.CurrentWeather;
using Core.Entities.Forecast;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;
using Microsoft.Extensions.Configuration;
public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherService> _logger;
    private readonly string _apiKey;
    public WeatherService(HttpClient http, IConfiguration configuration, ILogger<WeatherService> logger)
    {
        _httpClient = http;
        _logger = logger;
        _apiKey = configuration["WeatherAPI:ApiKey"];
    }
    
    public async Task<WeatherResponse> GetCurrentWeatherAsync(double lat, double lon)
    {
        var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid={_apiKey}";
        return await GetApiResponseAsync<WeatherResponse>(url);
    }

    public async Task<List<GeocodingResponse>> GetPlaceSuggestionsAsync(string cityName)
    {
        var url = $"https://api.openweathermap.org/geo/1.0/direct?q={Uri.EscapeDataString(cityName)}&limit=5&appid={_apiKey}";
        return await GetApiResponseAsync<List<GeocodingResponse>>(url) ?? new List<GeocodingResponse>();
    }

    public async Task<AirPollutionResponse> GetCurrentAirPollutionAsync(double lat, double lon)
    {
        var url = $"https://api.openweathermap.org/data/2.5/air_pollution?lat={lat}&lon={lon}&appid={_apiKey}";
        return await GetApiResponseAsync<AirPollutionResponse>(url);
    }

    public async Task<(List<NextDaysWeatherInfo> hourlyWeather, List<NextDaysWeatherInfo> dailyHighestTemp)> GetWeatherForCurrentAndNextDaysAsync(double lat, double lon)
    {
        var url = $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&appid={_apiKey}";
        var weatherResponse = await GetApiResponseAsync<NextDaysWeatherResponse>(url);
        
        var today = DateTime.UtcNow.Date;
        var hourlyWeatherToday = weatherResponse.WeatherInfoList
            .Where(w => DateTimeOffset.FromUnixTimeSeconds(w.DateTime).Date == today)
            .ToList();

        var dailyHighestTemp = weatherResponse.WeatherInfoList
            .GroupBy(w => DateTimeOffset.FromUnixTimeSeconds(w.DateTime).Date)
            .Select(g => g.OrderByDescending(w => w.Main.TempMax).First())
            .Take(5)
            .ToList();

        return (hourlyWeatherToday, dailyHighestTemp);
    }
    

    private async Task<T> GetApiResponseAsync<T>(string url)
    {
        try
        {
            _logger.LogInformation("Requesting data from {Url}", url);
            var responseString = await _httpClient.GetStringAsync(url);
            _logger.LogInformation("Received data: {Response}", responseString);
            return JsonSerializer.Deserialize<T>(responseString);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching data from {Url}", url);
            throw;
        }
    }
}
