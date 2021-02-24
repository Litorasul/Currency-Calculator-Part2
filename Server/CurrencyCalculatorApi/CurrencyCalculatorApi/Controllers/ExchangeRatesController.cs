namespace CurrencyCalculatorApi.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using CurrencyCalculatorApi.Services;
    using CurrencyCalculatorApi.ViewModels;

    [ApiController]
    [Route("/api/[controller]")]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IDatabaseService service;

        public ExchangeRatesController(IDatabaseService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<List<ExchangeRateViewModel>> GetRateForCurrency(string currencyCode)
        {
            var rates = this.service.GetAllRatesPerCurrency(currencyCode);
            if (rates.Count < 1)
            {
                return this.NotFound();
            }
            return this.Ok(rates);
        }
    }
}