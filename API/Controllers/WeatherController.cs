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
            var (lat, lon) = await _weatherService.GetCoordinatesAsync(cityName);
            var weather = await _weatherService.GetCurrentWeatherAsync(lat, lon);
            return Ok(weather);
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
}