namespace CurrencyCalculatorApi.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using CurrencyCalculatorApi.Data;
    using CurrencyCalculatorApi.Models;
    using CurrencyCalculatorApi.ViewModels;

    public class DatabaseService : IDatabaseService
    {
        private readonly ApplicationDbContext dbContext;

        public DatabaseService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ExchangeRateViewModel> GetAllRatesPerCurrency(string currencyCode)
        {
            List<ExchangeRateViewModel> rates = this.dbContext.ExchangeRates
                .Where(x => x.Currency.Code == currencyCode)
                .Select(x => new ExchangeRateViewModel
                {
                    Id = x.Id,
                    Rate = x.Rate,
                    DateTime = x.DateTime
                }).ToList();

            return rates;
        }

        public async Task CreateCurrencyAsync(string currencyCode)
        {
            var currency = new Currency
            {
                Code = currencyCode
            };

            await this.dbContext.Currencies.AddAsync(currency);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<int> CreateExchangeRateAsync(string currencyCode, decimal rate, DateTime date)
        {
            var currency = this.dbContext.Currencies.FirstOrDefault(x => x.Code == currencyCode);
            if (currency == null)
            {
                await this.CreateCurrencyAsync(currencyCode);
            }

            var exchangeRate = new ExchangeRate
            {
                Rate = rate,
                CurrencyCode = currencyCode,
                DateTime = date
            };

            await this.dbContext.ExchangeRates.AddAsync(exchangeRate);
            await this.dbContext.SaveChangesAsync();

            return exchangeRate.Id;
        }
    }
}