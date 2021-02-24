namespace CurrencyCalculatorApi.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using CurrencyCalculatorApi.Services;

    [ApiController]
    [Route("/api/[controller]")]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeService exchangeService;

        public ExchangeController(IExchangeService exchangeService)
        {
            this.exchangeService = exchangeService;
        }

        [HttpGet("latest")]
        public async Task<ActionResult<decimal>> GetLatest(string from, string to, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to) || amount <= 0)
            {
                return this.BadRequest();
            }

            var result = await this.exchangeService.ExchangeWithLatestRate(from, to, amount);
            return Ok(result);
        }

        [HttpGet("historical")]
        public async Task<ActionResult<decimal>> GetHistorical(string from, string to, decimal amount, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to) || amount <= 0)
            {
                return this.BadRequest();
            }

            var result = await this.exchangeService.ExchangeWithHistoricalRate(from, to, amount, date);
            return Ok(result);
        }
    }
}