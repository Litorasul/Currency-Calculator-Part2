namespace CurrencyCalculatorApi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using CurrencyCalculatorApi.Dtos;
    using CurrencyCalculatorApi.Services;

    using Moq;
    using Xunit;

    public class ExchangeServiceTests
    {
        [Fact]
        public async Task ExchangeWithLatestRateShouldWorkCorrectly()
        {
            var client = new Mock<IHttpClientFactory>();
            var fixer = new Mock<FixerService>(client.Object);
            fixer.Setup(f => f.GetLatestRateForEuroAsync())
                .ReturnsAsync(new FixerExchangeRateDto
                {
                    Base = "EUR",
                    Date = DateTime.Now,
                    Success = true,
                    Rates = new Dictionary<string, decimal>
                    {
                        {"NOK", 4},
                        {"BGN", 2}
                    }
                });
            var exchangeService = new ExchangeService(fixer.Object, new CalculatorService());
            var result = await exchangeService.ExchangeWithLatestRate("BGN", "NOK", 10);

            Assert.Equal(20, result);
        }

        [Fact]
        public async Task ExchangeWithHistoricalRateShouldWorkCorrectly()
        {
            var client = new Mock<IHttpClientFactory>();
            var fixer = new Mock<FixerService>(client.Object);
            fixer.Setup(f => f.GetHistoricalRateForEuroAsync(It.IsAny<string>()))
                .ReturnsAsync(new FixerExchangeRateDto
                {
                    Base = "EUR",
                    Date = DateTime.Now,
                    Success = true,
                    Rates = new Dictionary<string, decimal>
                    {
                        {"NOK", 4},
                        {"BGN", 2}
                    }
                });
            var exchangeService = new ExchangeService(fixer.Object, new CalculatorService());
            var result = await exchangeService.ExchangeWithHistoricalRate("BGN", "NOK", 10, DateTime.Now);

            Assert.Equal(20, result);
        }
    }
}