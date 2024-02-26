using RestApiTesting.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Test.xUnit.Validation
{
    public class AccountNumberValidationTests
    {
        private readonly AccountNumberValidation _validation;
        public AccountNumberValidationTests() => _validation = new AccountNumberValidation();
        [Fact]
        public void IsValid_ValidAccountNumber_ReturnsTrue()
            => Assert.True(_validation.IsValid("123-4543234576-23"));

        // is valid account number first part wrong format returns false using InlineData
        [Theory]
        [InlineData("1234-3454565676-23")]
        [InlineData("12-3454565676-23")]
        public void IsValid_InvalidAccountNumber_ReturnsFalse(string accountNumber)
            => Assert.False(_validation.IsValid(accountNumber));

        // is valid account number middle part wrong format returns false using InlineData
        [Theory]
        [InlineData("123-345456567-23")]
        [InlineData("123-345456567633-23")]
        public void IsValid_InvalidAccountNumber_ReturnsFalseMiddlePart(string accountNumber)
            => Assert.False(_validation.IsValid(accountNumber));

        // is valid account number last part wrong format returns false using InlineData
        [Theory]
        [InlineData("123-3434545656-2")]
        [InlineData("123-3454565676-234")]
        public void IsValid_InvalidAccountNumber_ReturnsFalseLastPart(string accountNumber)
            => Assert.False(_validation.IsValid(accountNumber));

        // is valid account number missing delimiter returns throw argument exception using InlineData
        [Theory]
        [InlineData("1233454565676-23")]
        [InlineData("123+345456567623")]
        [InlineData("123+3454565676=23")]
        public void IsValid_InvalidAccountNumber_ReturnsThrowArgumentException(string accountNumber)
            => Assert.Throws<ArgumentException>(() => _validation.IsValid(accountNumber));
    }
}
