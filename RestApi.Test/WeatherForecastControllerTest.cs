using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestApiTesting.Controllers;
using RestApiTesting.Models;
using RestApiTesting.Services;

namespace RestApi.Test
{
    public class WeatherForecastControllerTest
    {
        private Mock<IWeatherForecastService> _weatherForecastService;
        private WeatherForecastController _weatherForecastController;
        private ILogger<WeatherForecastController> _logger;
        public WeatherForecastControllerTest()
        {
            _weatherForecastService = new Mock<IWeatherForecastService>();
            _logger = new Mock<ILogger<WeatherForecastController>>().Object;
            _weatherForecastController = new WeatherForecastController(_logger, _weatherForecastService.Object);
        }

        // Write test for GetWeatherForecast method
        [Test]
        public void GetWeatherForecast_ReturnsData()
        {

            // Act
            var result = _weatherForecastController.Get();

            // Assert
            Assert.IsNotNull(result);

            // Assert that the result is of type IEnumerable<WeatherForecast>
            Assert.That(result.Count(), Is.EqualTo(5));

            // Assert that the result contains WeatherForecast objects
            foreach (var item in result)
            {
                Assert.IsInstanceOf<WeatherForecast>(item);
            }

            // Assert that the result contains WeatherForecast objects with the correct properties
            foreach (var item in result)
            {
                Assert.IsNotNull(item.Date);
                Assert.IsNotNull(item.TemperatureC);
                Assert.IsNotNull(item.Summary);
            }

            List<string> Summaries = new List<string>()
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
            // Assert that the result contains WeatherForecast objects with the correct values
            foreach (var item in result)
            {
                Assert.IsTrue(item.Date > DateOnly.FromDateTime(DateTime.Now));
                Assert.IsTrue(item.TemperatureC >= -20 && item.TemperatureC <= 55);
                Assert.That(Summaries, Does.Contain(item.Summary));
            }
        }

        // Write test for GetWeatherForecastById method
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
            _weatherForecastService.Setup(x => x.GetWeatherForecast(1)).ReturnsAsync(weatherForecast);

            // Act
            var result = await _weatherForecastController.GetWeatherForecast(1);

            // Assert
            Assert.IsNotNull(result);

            // Assert that the result is of type ActionResult<WeatherForecast>
            Assert.IsInstanceOf<ActionResult<WeatherForecast>>(result);

            // Assert that the result contains a WeatherForecast object
            Assert.IsInstanceOf<WeatherForecast>(result.Value);

            // Assert that the result contains a WeatherForecast object with the correct properties
            if (result.Value != null)
            {
                Assert.IsNotNull(result.Value.Date);
                Assert.IsNotNull(result.Value.TemperatureC);
                Assert.IsNotNull(result.Value.Summary);

                // Assert that the result contains a WeatherForecast object with the correct values
                Assert.That(result.Value.Id, Is.EqualTo(1));
                Assert.That(result.Value.Date, Is.EqualTo(weatherForecast.Date));
                Assert.That(result.Value.TemperatureC, Is.EqualTo(weatherForecast.TemperatureC));
                Assert.That(result.Value.Summary, Is.EqualTo(weatherForecast.Summary));
            }
        }

        // Write test for PostWeatherForecast method
        [Test]
        public async Task PostWeatherForecast_ReturnsData()
        {
            // Arrange
            var weatherForecast = new WeatherForecast
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 20,
                Summary = "Mild"
            };
            _weatherForecastService.Setup(x => x.AddWeatherForecast(weatherForecast)).ReturnsAsync(weatherForecast);

            // Act
            var result = await _weatherForecastController.PostWeatherForecast(weatherForecast);

            // Assert
            Assert.IsNotNull(result);

            // Assert that the result is of type ActionResult<WeatherForecast>
            Assert.IsInstanceOf<ActionResult<WeatherForecast>>(result);

            // Assert that the result contains a WeatherForecast object
            Assert.IsInstanceOf<WeatherForecast>(result.Value);

            // Assert that the result contains a WeatherForecast object with the correct properties
            Assert.IsNotNull(result.Value.Date);
            Assert.IsNotNull(result.Value.TemperatureC);
            Assert.IsNotNull(result.Value.Summary);

            // Assert that the result contains a WeatherForecast object with the correct values
            Assert.AreEqual(1, result.Value.Id);
            Assert.AreEqual(weatherForecast.Date, result.Value.Date);
            Assert.AreEqual(weatherForecast.TemperatureC, result.Value.TemperatureC);
            Assert.AreEqual(weatherForecast.Summary, result.Value.Summary);
        }

        // Write test for PutWeatherForecast method
        [Test]
        public async Task PutWeatherForecast_ReturnsData()
        {
            // Arrange
            var weatherForecast = new WeatherForecast
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 20,
                Summary = "Mild"
            };
            _weatherForecastService.Setup(x => x.GetWeatherForecast(1)).ReturnsAsync(weatherForecast);
            _weatherForecastService.Setup(x => x.UpdateWeatherForecast(weatherForecast));

            // Act
            var result = await _weatherForecastController.PutWeatherForecast(weatherForecast);

            // Assert
            Assert.IsNotNull(result);

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
            _weatherForecastService.Setup(x => x.GetWeatherForecast(1)).ReturnsAsync(weatherForecast);
            _weatherForecastService.Setup(x => x.DeleteWeatherForecast(1)).ReturnsAsync(weatherForecast);   
            // Act
            var result = await _weatherForecastController.DeleteWeatherForecast(1);

            // Assert
            Assert.IsNotNull(result);

            // Assert that the result is of type ActionResult<WeatherForecast>
            Assert.IsInstanceOf<ActionResult<WeatherForecast>>(result);

            // Assert that the result contains a WeatherForecast object
            Assert.IsInstanceOf<WeatherForecast>(result.Value);

            // Assert that the result contains a WeatherForecast object with the correct properties
            Assert.IsNotNull(result.Value.Date);
            Assert.IsNotNull(result.Value.TemperatureC);
            Assert.IsNotNull(result.Value.Summary);

            // Assert that the result contains a WeatherForecast object with the correct values
            Assert.AreEqual(1, result.Value.Id);
            Assert.AreEqual(weatherForecast.Date, result.Value.Date);
            Assert.AreEqual(weatherForecast.TemperatureC, result.Value.TemperatureC);
            Assert.AreEqual(weatherForecast.Summary, result.Value.Summary);
        }
    }
}