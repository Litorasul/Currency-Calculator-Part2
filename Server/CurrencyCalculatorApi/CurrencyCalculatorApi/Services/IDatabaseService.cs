namespace CurrencyCalculatorApi.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using CurrencyCalculatorApi.ViewModels;
    public interface IDatabaseService
    {
        List<ExchangeRateViewModel> GetAllRatesPerCurrency(string currencyCode);

        Task CreateCurrencyAsync(string currencyCode);

        Task<int> CreateExchangeRateAsync(string currencyCode, decimal rate, DateTime date);
    }
}