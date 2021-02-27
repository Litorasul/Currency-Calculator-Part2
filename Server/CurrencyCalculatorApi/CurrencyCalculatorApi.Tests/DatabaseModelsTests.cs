namespace CurrencyCalculatorApi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CurrencyCalculatorApi.Models;

    using Xunit;

    public class DatabaseModelsTests
    {
        [Fact]
        public void ExchangeRateShouldHaveCurrencyCode()
        {
            var rate = new ExchangeRate()
            {
                CurrencyCode = null,
                Rate = 2m,
                DateTime = DateTime.Now
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(rate, new ValidationContext(rate), validatorResults, true);


            Assert.False(actual);
            Assert.Single(validatorResults);
        }
    }
}