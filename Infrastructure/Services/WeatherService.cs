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
    //Preprocessing
    public async Task<(List<NextDaysWeatherInfo> hourlyWeather, List<NextDaysWeatherInfo> dailyHighestTemp)> GetWeatherForCurrentAndNextDaysAsync(double lat, double lon)
    {
        var url = $"http://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&appid={_apiKey}";
        var responseString = await _httpClient.GetStringAsync(url);
        var weatherResponse = JsonSerializer.Deserialize<NextDaysWeatherResponse>(responseString);

        // Get today's date
        var today = DateTime.UtcNow.Date;

        // Separate hourly weather for today and highest temp for each of the next 5 days
        var hourlyWeatherToday = new List<NextDaysWeatherInfo>();
        var dailyHighestTemp = new List<NextDaysWeatherInfo>();

        var dailyTemps = new Dictionary<DateTime, NextDaysWeatherInfo>();

        foreach (var weatherInfo in weatherResponse.WeatherInfoList)
        {
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(weatherInfo.DateTime).DateTime;

            if (dateTime.Date == today)
            {
                // Add to hourly weather for today
                hourlyWeatherToday.Add(weatherInfo);
            }

            // Determine the highest temperature for each day
            var date = dateTime.Date;
            if (!dailyTemps.ContainsKey(date) || dailyTemps[date].Main.TempMax < weatherInfo.Main.TempMax)
            {
                dailyTemps[date] = weatherInfo;
            }
        }

        // The API returns data for 5 days, so we take the highest temps from the first 5 days
        dailyHighestTemp = dailyTemps.Values.Take(5).ToList();

        return (hourlyWeatherToday, dailyHighestTemp);
    }
}
