namespace CurrencyCalculatorApi.Services
{
    using CurrencyCalculatorApi.Dtos;
    public class CalculatorService : ICalculatorService
    {
        public decimal ConvertToEuro( string currencyCode, decimal amount, FixerExchangeRateDto rate)
        {
            if (currencyCode == "EUR")
            {
                return amount;
            }

            return amount / rate.Rates[currencyCode];
        }

        public decimal Convert(string fromCurrencyCode, string toCurrencyCode, decimal amount, FixerExchangeRateDto rate)
        {
            if (fromCurrencyCode == toCurrencyCode)
            {
                return amount;
            }

            var inEuro = this.ConvertToEuro(fromCurrencyCode, amount, rate);
            return inEuro * rate.Rates[toCurrencyCode];
        }
    }
}