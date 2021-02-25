using CurrencyCalculatorApi.ViewModels;

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
        public async Task<ActionResult<List<CurrenciesViewModel>>> GetAvailableCurrencies()
        {
            var currencies = await this.service.GetAvailableCurrenciesAsync();
            if (!currencies.Success)
            {
                return this.NotFound();
            }
            List<CurrenciesViewModel> result = new List<CurrenciesViewModel>();
            foreach (var currency in currencies.symbols)
            {
                var model = new CurrenciesViewModel
                {
                    Code = currency.Key,
                    Name = currency.Value
                };
                result.Add(model);
            }

            return this.Ok(result);
        }
    }
}