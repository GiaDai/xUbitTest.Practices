using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using RestApiTesting.Controllers;

namespace RestApi.Test.xUnit.Controller
{
    public class AuthenControllerTest
    {
        private readonly AuthenController _authenController;
        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
        public AuthenControllerTest()
        {
            _authenController = new AuthenController(_configuration.Object);
        }

        // Write test for Login Action Method has email and password is correct return token string
        [Fact]
        public void Login_EmailAndPasswordIsCorrect_ReturnTokenString()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "admin@gmail.com", Password = "P@ssw0rd" };

            // Act
            var request = _authenController.Login(loginRequest);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(request);
            var tokenJwtString = (TokenJwt)okResult.Value;
            Assert.NotNull(tokenJwtString);
        }

        // Write test has email and password parameters for Login Action Method has email and password is incorrect return 400 BadRequest with InlineData
        [Theory]
        [InlineData(null, "P@ssw0rd")]
        [InlineData("admin@gmail.com", "IncorrectP@ssw0rd")]
        [InlineData("incorrect@gmail.com", "P@ssw0rd")]
        public void Login_EmailAndPasswordIsIncorrect_Return400BadRequest(string email, string password)
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = email, Password = password };

            // Act
            var request = _authenController.Login(loginRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(request);
            // Read message from BadRequestObjectResult
            var badRequest = Assert.IsType<BadRequestObjectResult>(request);
        }
    }
}
