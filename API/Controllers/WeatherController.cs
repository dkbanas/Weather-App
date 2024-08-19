using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentWeather(double lat, double lon)
    {
        try
        {
            var weather = await _weatherService.GetCurrentWeatherAsync(lat, lon);
            return Ok(weather);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [HttpGet("currentByCity")]
    public async Task<IActionResult> GetCurrentWeatherByCity(string cityName)
    {
        try
        {
            var locationData = await _weatherService.GetPlaceSuggestionsAsync(cityName);
            if (!locationData.Any())
            {
                return NotFound(new { error = "No places found" });
            }

            var firstLocation = locationData.First();
            var weather = await _weatherService.GetCurrentWeatherAsync(firstLocation.lat, firstLocation.lon);
            return Ok(weather);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [HttpGet("places")]
    public async Task<IActionResult> GetPlaceSuggestions(string query)
    {
        try
        {
            var locationData = await _weatherService.GetPlaceSuggestionsAsync(query);
            if (!locationData.Any())
            {
                return NotFound(new { error = "No places found" });
            }

            // Group by name, country, and state to remove duplicates, but retain the latitude and longitude of the first occurrence
            var distinctSuggestions = locationData
                .GroupBy(location => new { location.name, location.country, location.state })
                .Select(g => g.First())
                .Select(location => new
                {
                    location.name,
                    location.lat,
                    location.lon,
                    location.country,
                    location.state
                })
                .ToList();

            return Ok(distinctSuggestions);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [HttpGet("currentAirPollution")]
    public async Task<IActionResult> GetCurrentAirPollution(double lat, double lon)
    {
        var airPollution = await _weatherService.GetCurrentAirPollutionAsync(lat, lon);
        return Ok(airPollution);
    }
    
    [HttpGet("prediction")]
    public async Task<IActionResult> WeatherForNextDays(double lat, double lon)
    {
        var (hourlyWeatherToday, dailyHighestTemp) = await _weatherService.GetWeatherForCurrentAndNextDaysAsync(lat, lon);
        var result = new
        {
            HourlyWeatherToday = hourlyWeatherToday,
            DailyHighestTemp = dailyHighestTemp
        };
        return Ok(result);
    }
}