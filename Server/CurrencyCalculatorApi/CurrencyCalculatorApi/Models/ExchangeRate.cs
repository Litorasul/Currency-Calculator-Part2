namespace CurrencyCalculatorApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    // The base currency is Euro, so all the ExchangeRates are to EUR
    public class ExchangeRate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [ForeignKey("Currency")]
        public string CurrencyCode { get; set; }

        public virtual Currency Currency { get; set; }
    }
}