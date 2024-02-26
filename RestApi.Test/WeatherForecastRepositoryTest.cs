using Moq;
using RestApiTesting.Models;
using RestApiTesting.Services;

namespace RestApi.Test
{
    public class WeatherForecastRepositoryTest
    {
        private Mock<RestApiTestingContext> _context;
        private WeatherForecastRepository _weatherForecastRepository;
        public WeatherForecastRepositoryTest()
        {
            _context = new Mock<RestApiTestingContext>();
            _weatherForecastRepository = new WeatherForecastRepository(_context.Object);
        }

        // Write test for AddWeatherForecast method
        [Test]
        public async Task AddWeatherForecast_ReturnsData()
        {
            // Arrange
            var weatherForecast = new WeatherForecast
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 20,
                Summary = "Mild"
            };
            _context.Setup(x => x.Add(weatherForecast));
            _context.Setup(x => x.SaveChanges());

            // Act
            var result = await _weatherForecastRepository.AddWeatherForecast(weatherForecast);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<WeatherForecast>(result);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Date, Is.EqualTo(DateOnly.FromDateTime(DateTime.Now)));
            Assert.That(result.TemperatureC, Is.EqualTo(20));
            Assert.That(result.Summary, Is.EqualTo("Mild"));
        }

        // Write test for DeleteWeatherForecast method
        [Test]
        public async Task DeleteWeatherForecast_ReturnsData()
        {
            // Arrange
            var weatherForecast = new WeatherForecast
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 20,
                Summary = "Mild"
            };
            _context.Setup(x => x.WeatherForecasts.Find(1)).Returns(weatherForecast);
            _context.Setup(x => x.WeatherForecasts.Remove(weatherForecast));
            _context.Setup(x => x.SaveChanges());

            // Act
            var result = await _weatherForecastRepository.DeleteWeatherForecast(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<WeatherForecast>(result);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Date, Is.EqualTo(DateOnly.FromDateTime(DateTime.Now)));
            Assert.That(result.TemperatureC, Is.EqualTo(20));
            Assert.That(result.Summary, Is.EqualTo("Mild"));
        }

        // Write test for GetWeatherForecast by id method
        [Test]
        public async Task GetWeatherForecastById_ReturnsData()
        {
            // Arrange
            var weatherForecast = new WeatherForecast
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 20,
                Summary = "Mild"
            };
            _context.Setup(x => x.WeatherForecasts.FindAsync(1)).ReturnsAsync(weatherForecast);

            // Act
            var result = await _weatherForecastRepository.GetWeatherForecast(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<WeatherForecast>(result);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Date, Is.EqualTo(DateOnly.FromDateTime(DateTime.Now)));
            Assert.That(result.TemperatureC, Is.EqualTo(20));
            Assert.That(result.Summary, Is.EqualTo("Mild"));
        }

        // Write test for UpdateWeatherForecast method
        [Test]
        public async Task UpdateWeatherForecast_ReturnsData()
        {
            // Arrange
            var weatherForecast = new WeatherForecast
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 20,
                Summary = "Mild"
            };
            _context.Setup(x => x.Update(weatherForecast));
            _context.Setup(x => x.SaveChanges());

            // Act
            await _weatherForecastRepository.UpdateWeatherForecast(weatherForecast);

            // Assert
            Assert.Pass();
        }
    }
}
