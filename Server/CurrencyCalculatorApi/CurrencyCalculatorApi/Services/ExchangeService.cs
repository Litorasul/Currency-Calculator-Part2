namespace CurrencyCalculatorApi.Services
{
    using System;
    using System.Threading.Tasks;

    public class ExchangeService : IExchangeService
    {
        private readonly IFixerService fixer;
        private readonly ICalculatorService calculator;

        public ExchangeService(IFixerService fixer, ICalculatorService calculator)
        {
            this.fixer = fixer;
            this.calculator = calculator;
        }

        public async Task<decimal> ExchangeWithLatestRate(string fromCurrencyCode, string toCurrencyCode, decimal amount)
        {
            var rate = await this.fixer.GetLatestRateForEuroAsync();
            return this.calculator.Convert(fromCurrencyCode, toCurrencyCode, amount, rate);
        }

        public async Task<decimal> ExchangeWithHistoricalRate(string fromCurrencyCode, string toCurrencyCode,
            decimal amount, DateTime date)
        {
            var rate = await this.fixer.GetHistoricalRateForEuroAsync(date.ToString("yyyy-MM-dd"));
            return this.calculator.Convert(fromCurrencyCode, toCurrencyCode, amount, rate);
        }
    }
}