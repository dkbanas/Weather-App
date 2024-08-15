using System.Text.Json;
using Core.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _weatherService;

    public WeatherController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentWeather(double lat, double lon)
    {
        var weather = await _weatherService.GetCurrentWeatherAsync(lat, lon);
        Console.WriteLine(JsonSerializer.Serialize(weather));
        return Ok(weather);
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

    // Endpoint to get place suggestions
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

            var suggestions = locationData.Select(location => new
            {
                name = location.name,
                lat = location.lat,
                lon = location.lon,
                country = location.country,
                state = location.state
            }).ToList();

            return Ok(suggestions);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    //Air Pollution
    [HttpGet("currentAirPollution")]
    public async Task<IActionResult> GetCurrentAirPollution(double lat, double lon)
    {
        var airPollution = await _weatherService.GetCurrentAirPollutionAsync(lat, lon);
        Console.WriteLine(JsonSerializer.Serialize(airPollution));
        return Ok(airPollution);
    }
    
    //NextDaysWeather
    [HttpGet("prediction")]
    public async Task<IActionResult> WeatherForNextDays(double lat, double lon)
    {
        var weather = await _weatherService.GetWeatherForNextDaysAsync(lat, lon);
        Console.WriteLine(JsonSerializer.Serialize(weather));
        return Ok(weather);
    }
}