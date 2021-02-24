namespace CurrencyCalculatorApi.Services
{
    using System;
    using System.Threading.Tasks;

    public interface IExchangeService
    {
        Task<decimal> ExchangeWithLatestRate(string fromCurrencyCode, string toCurrencyCode, decimal amount);

        Task<decimal> ExchangeWithHistoricalRate(string fromCurrencyCode, string toCurrencyCode, decimal amount,
            DateTime date);
    }
}