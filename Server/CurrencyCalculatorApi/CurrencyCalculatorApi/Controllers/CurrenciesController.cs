namespace CurrencyCalculatorApi.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    
    using CurrencyCalculatorApi.Services;

    [ApiController]
    [Route("/api/[controller]")]
    public class CurrenciesController : ControllerBase
    {
        private readonly IFixerService service;

        public CurrenciesController(IFixerService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Dictionary<string, string>>> GetAvailableCurrencies()
        {
            var currencies = await this.service.GetAvailableCurrenciesAsync();
            if (!currencies.Success)
            {
                return this.NotFound();
            }

            return this.Ok(currencies.symbols);
        }
    }
}