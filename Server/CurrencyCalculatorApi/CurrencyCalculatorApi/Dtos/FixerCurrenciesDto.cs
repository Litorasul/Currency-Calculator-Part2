namespace CurrencyCalculatorApi.Dtos
{
    using System.Collections.Generic;

    public class FixerCurrenciesDto
    {
        public bool Success { get; set; }

        public Dictionary<string, string> symbols { get; set; }
    }
}