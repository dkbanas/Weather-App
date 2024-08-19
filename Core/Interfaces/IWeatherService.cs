using Core.Entities;
using Core.Entities.AirPollution;
using Core.Entities.CurrentWeather;
using Core.Entities.Forecast;

namespace Core.Interfaces;

public interface IWeatherService
{
    Task<WeatherResponse> GetCurrentWeatherAsync(double lat, double lon);
    Task<List<GeocodingResponse>> GetPlaceSuggestionsAsync(string cityName);
    Task<AirPollutionResponse> GetCurrentAirPollutionAsync(double lat, double lon);
    Task<(List<NextDaysWeatherInfo> hourlyWeather, List<NextDaysWeatherInfo> dailyHighestTemp)> GetWeatherForCurrentAndNextDaysAsync(double lat, double lon);
}