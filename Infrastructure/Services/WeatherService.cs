using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Services;
using Microsoft.Extensions.Configuration;
public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    public WeatherService(HttpClient http, IConfiguration configuration)
    {
        _httpClient = http;
        _apiKey = configuration["WeatherAPI:ApiKey"];
    }
    
    public async Task<WeatherResponse> GetCurrentWeatherAsync(double lat, double lon)
    {
        var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid={_apiKey}";
        var responseString = await _httpClient.GetStringAsync(url);
        Console.WriteLine(responseString);
        var weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(responseString);
        return weatherResponse;
    }
    
    public async Task<List<GeocodingResponse>> GetPlaceSuggestionsAsync(string cityName)
    {
        var url = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=5&appid={_apiKey}";
        var responseString = await _httpClient.GetStringAsync(url);
        var locationData = JsonSerializer.Deserialize<List<GeocodingResponse>>(responseString);
        return locationData ?? new List<GeocodingResponse>();
    }
    
    //Air polution
    public async Task<AirPollutionResponse> GetCurrentAirPollutionAsync(double lat, double lon)
    {
        var url = $"http://api.openweathermap.org/data/2.5/air_pollution?lat={lat}&lon={lon}&appid={_apiKey}";
        var responseString = await _httpClient.GetStringAsync(url);
        Console.WriteLine(responseString);
        var airPollutionResponse = JsonSerializer.Deserialize<AirPollutionResponse>(responseString);
        return airPollutionResponse;
    }
    //5 days weather
    public async Task<NextDaysWeatherResponse> GetWeatherForNextDaysAsync(double lat, double lon)
    {
        var url = $"http://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&appid={_apiKey}";
        var responseString = await _httpClient.GetStringAsync(url);
        Console.WriteLine(responseString);
        var weatherResponse = JsonSerializer.Deserialize<NextDaysWeatherResponse>(responseString);
        return weatherResponse;
        
    }
}
