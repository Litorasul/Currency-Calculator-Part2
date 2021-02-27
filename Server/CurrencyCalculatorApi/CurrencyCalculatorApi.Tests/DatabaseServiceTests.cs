namespace CurrencyCalculatorApi.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using CurrencyCalculatorApi.Data;
    using CurrencyCalculatorApi.Models;
    using CurrencyCalculatorApi.Services;

    using Xunit;


    public class DatabaseServiceTests
    {
        private readonly IDatabaseService dbService;
        private readonly ApplicationDbContext dbContext;

        public DatabaseServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);

            this.dbService = new DatabaseService(dbContext);
        }

        [Fact]
        public async Task GetAllRatesPerCurrencyShouldWorkCorrectly()
        {
            await this.PopulateDatabase();

            var models = this.dbService.GetAllRatesPerCurrency("NOK");

            Assert.Equal(2, models.Count);
            Assert.Equal(1, models[0].Id);
            Assert.Equal(2, models[1].Id);
        }

        [Fact]
        public async Task CreateCurrencyAsyncShouldWorkCorrectly()
        {
            await this.dbService.CreateCurrencyAsync("USD");
            await this.dbService.CreateCurrencyAsync("BGN");
            
            var result = this.dbContext.Currencies.Where(x => x.Code == "USD").ToList();

            Assert.Single(result);
        }

        [Fact]
        public async Task CreateExchangeRateAsyncShouldCreateExchangeRate()
        {
            await this.dbService.CreateCurrencyAsync("USD");

            await this.dbService.CreateExchangeRateAsync("USD", 1.7m, DateTime.Now);

            var result = this.dbContext.ExchangeRates
                .Where(x => x.CurrencyCode == "USD").ToList();

            Assert.Single(result);
            Assert.Equal(1.7m, result[0].Rate);
        }

        [Fact]
        public async Task CreateExchangeRateAsyncShouldCreateCurrency()
        {
            await this.dbService.CreateExchangeRateAsync("USD", 1.7m, DateTime.Now);

            var result = this.dbContext.Currencies.Where(x => x.Code == "USD").ToList();

            Assert.Single(result);
            Assert.Equal("USD", result[0].Code);
        }


        private async Task PopulateDatabase()
        {
            var kroner = new Currency
            {
                Code = "NOK"
            };

            var leva = new Currency
            {
                Code = "BGN"
            };

            await this.dbContext.Currencies.AddAsync(kroner);
            await this.dbContext.Currencies.AddAsync(leva);

            var rate1 = new ExchangeRate
            {
                Id = 1,
                Rate = 4m,
                DateTime = DateTime.Now,
                CurrencyCode = "NOK"
            };

            var rate2 = new ExchangeRate
            {
                Id = 2,
                Rate = 4m,
                DateTime = DateTime.Now,
                CurrencyCode = "NOK"
            };

            var rate3 = new ExchangeRate
            {
                Id = 3,
                Rate = 2m,
                DateTime = DateTime.Now,
                CurrencyCode = "BGN"
            };

            var rate4 = new ExchangeRate
            {
                Id = 4,
                Rate = 2m,
                DateTime = DateTime.Now,
                CurrencyCode = "BGN"
            };

            await this.dbContext.ExchangeRates.AddAsync(rate1);
            await this.dbContext.ExchangeRates.AddAsync(rate2);
            await this.dbContext.ExchangeRates.AddAsync(rate3);
            await this.dbContext.ExchangeRates.AddAsync(rate4);
            await this.dbContext.SaveChangesAsync();
        }
    }
}