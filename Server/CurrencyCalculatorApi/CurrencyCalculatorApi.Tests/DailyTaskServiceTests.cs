namespace CurrencyCalculatorApi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using CurrencyCalculatorApi.Data;
    using CurrencyCalculatorApi.Dtos;
    using CurrencyCalculatorApi.Services;

    using Microsoft.EntityFrameworkCore;

    using Moq;
    using Xunit;

    public class DailyTaskServiceTests
    {
        private readonly ApplicationDbContext dbContext;

        public DailyTaskServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);
        }

        [Fact]
        public async Task StoreLatestExchangeRateAsyncShouldWorkCorrectly()
        {
            List<string> result = new List<string>();

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
            var dbService = new Mock<DatabaseService>(this.dbContext);
            dbService
                .Setup(s => s.CreateExchangeRateAsync(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<DateTime>()))
                .Callback((string s, decimal d, DateTime dd) => result.Add(s));

            var daily = new DailyTasksService(dbService.Object, fixer.Object);

            await daily.StoreLatestExchangeRateAsync();

            Assert.Equal(2, result.Count);
        }
    }
}