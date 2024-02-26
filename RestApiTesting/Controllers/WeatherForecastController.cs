using Microsoft.AspNetCore.Mvc;
using RestApiTesting.Models;
using RestApiTesting.Services;

namespace RestApiTesting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IWeatherForecastService _weatherForecastRepository;
        private readonly ILogger<WeatherForecastController> _logger;


        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IWeatherForecastService weatherForecastRepository)
        {
            _logger = logger;
            _weatherForecastRepository = weatherForecastRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // Implement the GetWeatherForecastById method that takes in an id and returns a WeatherForecast object
        [HttpGet("{id}", Name = "GetWeatherForecastById")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(int id)
        {
            var weatherForecast = await _weatherForecastRepository.GetWeatherForecast(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }
            return weatherForecast;
        }

        // Implement the PostWeatherForecast method that takes in a WeatherForecast object and returns a WeatherForecast object
        [HttpPost(Name = "PostWeatherForecast")]
        public async Task<ActionResult<WeatherForecast>> PostWeatherForecast([FromBody]WeatherForecast weatherForecast)
        {
            var newWeatherForecast = await _weatherForecastRepository.AddWeatherForecast(weatherForecast);
            return newWeatherForecast;
        }

        // Implement the PutWeatherForecast method that takes in a WeatherForecast object and returns a WeatherForecast object
        [HttpPut(Name = "PutWeatherForecast")]
        public async Task<ActionResult<WeatherForecast>> PutWeatherForecast([FromBody]WeatherForecast weatherForecast)
        {
            // Find the weather forecast by id
            var existingWeatherForecast = await _weatherForecastRepository.GetWeatherForecast(weatherForecast.Id);
            if (existingWeatherForecast == null)
            {
                return NotFound();
            }
            await _weatherForecastRepository.UpdateWeatherForecast(weatherForecast);
            return Ok();
        }

        // Implement the DeleteWeatherForecast method that takes in an id and returns a WeatherForecast object
        [HttpDelete("{id}", Name = "DeleteWeatherForecast")]
        public async Task<ActionResult<WeatherForecast>> DeleteWeatherForecast(int id)
        {
            var weatherForecast = await _weatherForecastRepository.GetWeatherForecast(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }
            var deletedWeatherForecast = await _weatherForecastRepository.DeleteWeatherForecast(id);
            return deletedWeatherForecast;
        }
    }
}
