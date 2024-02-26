using RestApiTesting.Validation;

namespace RestApi.Test.Validation
{
    public class AccountNumberValidationTests
    {
        private readonly AccountNumberValidation _accountNumberValidation;
        public AccountNumberValidationTests()
        {
            _accountNumberValidation = new AccountNumberValidation();
        }

        [Test]
        public void IsValid_ValidAccountNumber_ReturnsTrue()
        {
            // Arrange
            var accountNumber = "123-4356874310-43";

            // Act
            var result = _accountNumberValidation.IsValid(accountNumber);

            // Assert
            Assert.True(result);
        }
    }
}
