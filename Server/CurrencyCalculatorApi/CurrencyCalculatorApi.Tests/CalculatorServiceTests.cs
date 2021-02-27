namespace CurrencyCalculatorApi.Tests
{
    using System;
    using System.Collections.Generic;

    using CurrencyCalculatorApi.Dtos;
    using CurrencyCalculatorApi.Services;

    using Xunit;

    public class CalculatorServiceTests
    {
        private readonly ICalculatorService calculator;
        private readonly FixerExchangeRateDto rate;

        public CalculatorServiceTests()
        {
            this.rate = new FixerExchangeRateDto
            {
                Base = "EUR",
                Date = DateTime.Now,
                Success = true,
                Rates = new Dictionary<string, decimal>()
            };

            rate.Rates["BGN"] = 2m;
            rate.Rates["NOK"] = 10m;

            this.calculator = new CalculatorService();
        }
        
        [Theory]
        [InlineData("BGN", 20, 10)]
        [InlineData("NOK", 100, 10)]
        [InlineData("EUR", 23, 23)]
        public void ConvertToEuroShouldWorkCorrectly(string code, decimal amount, decimal expected)
        {
            var actual = this.calculator.ConvertToEuro(code, amount, this.rate);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("BGN", "NOK", 10, 50)]
        [InlineData("EUR", "BGN", 10, 20)]
        [InlineData("NOK", "BGN", 100, 20)]
        public void ConvertShouldWorkCorrectly(string from, string to, decimal amount, decimal expected)
        {
            var actual = this.calculator.Convert(from, to, amount, this.rate);

            Assert.Equal(expected, actual);
        }
    }
}
