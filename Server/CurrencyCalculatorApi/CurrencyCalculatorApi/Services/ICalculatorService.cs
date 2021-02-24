namespace CurrencyCalculatorApi.Services
{
    using CurrencyCalculatorApi.Dtos;
    public interface ICalculatorService
    {
        decimal ConvertToEuro(string currencyCode, decimal amount, FixerExchangeRateDto rate);

        decimal Convert(string fromCurrencyCode, string toCurrencyCode, decimal amount, FixerExchangeRateDto rate);
    }
}