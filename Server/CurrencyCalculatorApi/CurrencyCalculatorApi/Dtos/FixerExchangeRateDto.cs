namespace CurrencyCalculatorApi.Dtos
{
    using System;
    using System.Collections.Generic;

    public class FixerExchangeRateDto
    {
        public bool Success { get; set; }

        public DateTime Date { get; set; }

        public string Base { get; set; }

        public Dictionary<string, decimal> Rates { get; set; }
    }
}