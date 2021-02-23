namespace CurrencyCalculatorApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    // The base currency is Euro, so all the ExchangeRates are to EUR
    public class Currency
    {
        [Key]
        [Required]
        public string Code { get; set; }

        public virtual ICollection<ExchangeRate> ExchangeRates { get; set; }
    }
}