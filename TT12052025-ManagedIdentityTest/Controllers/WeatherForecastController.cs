using Microsoft.AspNetCore.Mvc;
using TT12052025_ManagedIdentityTest.Models;
using Microsoft.EntityFrameworkCore;

namespace TT12052025_ManagedIdentityTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly WeatherForecastContext _context;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            /*return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            */
            var forecasts = await _context.WeatherForecasts.ToListAsync();
            return forecasts;
        }
        [HttpGet("{id}")]
        public async Task<WeatherForecast> Get(int id)
        {
            var forecast = await _context.WeatherForecasts.FindAsync(id);
            if (forecast == null)
            {
                throw new Exception("WeatherForecast not found");
            }
            return forecast;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
            {
                return BadRequest("WeatherForecast is null");
            }
            else
            {
                await _context.WeatherForecasts.AddAsync(forecast);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var forecast = await _context.WeatherForecasts.FindAsync(id);
            if (forecast == null)
            {
                return NotFound();
            }
            _context.WeatherForecasts.Remove(forecast);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
