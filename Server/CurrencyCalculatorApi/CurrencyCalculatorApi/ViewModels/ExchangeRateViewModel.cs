namespace CurrencyCalculatorApi.ViewModels
{
    using System;

    // The base currency is Euro, so all the ExchangeRates are to EUR
    public class ExchangeRateViewModel
    {
        public int Id { get; set; }

        public decimal Rate { get; set; }

        public string DateTime { get; set; }
    }
}