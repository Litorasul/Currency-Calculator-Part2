namespace CurrencyCalculatorApi.Services
{
    using System.Threading.Tasks;

    using CurrencyCalculatorApi.Dtos;
    public interface IFixerService
    {
        Task<FixerCurrenciesDto> GetAvailableCurrenciesAsync();

        Task<FixerExchangeRateDto> GetLatestRateForEuroAsync();

        /// <summary>
        /// Date should be in "yyyy-MM-dd" format.
        /// </summary>
        Task<FixerExchangeRateDto> GetHistoricalRateForEuroAsync(string date);
    }
}