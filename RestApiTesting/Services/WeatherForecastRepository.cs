using Microsoft.EntityFrameworkCore;
using RestApiTesting.Models;

namespace RestApiTesting.Services
{
    public class WeatherForecastRepository : IWeatherForecastService
    {
        // Initial RestApiTestingContext
        private readonly RestApiTestingContext _context;

        //Constructor for WeatherForecastRepository that takes in a RestApiTestingContext
        public WeatherForecastRepository(RestApiTestingContext context)
        {
            _context = context;
        }

        public async Task<WeatherForecast> AddWeatherForecast(WeatherForecast weatherForecast)
        {
            await _context.AddAsync(weatherForecast);
            await _context.SaveChangesAsync();
            return weatherForecast;
        }

        public async Task<WeatherForecast?> DeleteWeatherForecast(int id)
        {
            var weatherForecast = _context.WeatherForecasts.Find(id);
            if (weatherForecast == null)
            {
                return null;
            }
            _context.WeatherForecasts.Remove(weatherForecast);
            await _context.SaveChangesAsync();
            return weatherForecast;
        }

        public async Task<WeatherForecast> GetWeatherForecast(int id)
        {
            // Implement the GetWeatherForecast asyn await method
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);
            return weatherForecast;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
        {
            // Implement the list all WeatherForecast objects asyn await method
            return await _context.WeatherForecasts.ToListAsync();
        }

        public async Task UpdateWeatherForecast(WeatherForecast weatherForecast)
        {
            // Implement the UpdateWeatherForecast asyn await method
            _context.Update(weatherForecast);
            await _context.SaveChangesAsync();
        }

    }

    public interface IWeatherForecastService
    {
        // List all WeatherForecast objects use asyn to avoid blocking the main thread
        Task<IEnumerable<WeatherForecast>> GetWeatherForecasts();

        // Get a WeatherForecast object by id use asyn to avoid blocking the main thread
        Task<WeatherForecast?> GetWeatherForecast(int id);

        // Add a WeatherForecast object use asyn to avoid blocking the main thread
        Task<WeatherForecast> AddWeatherForecast(WeatherForecast weatherForecast);

        // Update a WeatherForecast object use asyn to avoid blocking the main thread
        Task UpdateWeatherForecast(WeatherForecast weatherForecast);

        // Delete a WeatherForecast object use asyn to avoid blocking the main thread
        Task<WeatherForecast?> DeleteWeatherForecast(int id);
    }
}
