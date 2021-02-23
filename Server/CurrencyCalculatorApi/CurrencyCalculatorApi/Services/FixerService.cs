namespace CurrencyCalculatorApi.Services
{
    
    using System.Net.Http;
    using System.Threading.Tasks;

    using CurrencyCalculatorApi.Common;
    using CurrencyCalculatorApi.Dtos;
    using static CurrencyCalculatorApi.Common.GlobalConstants;

    using Newtonsoft.Json;

    public class FixerService : IFixerService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly FixerSettings settings;

        public FixerService(IHttpClientFactory clientFactory, FixerSettings settings)
        {
            this.clientFactory = clientFactory;
            this.settings = settings;
        }

        public async Task<FixerCurrenciesDto> GetAvailableCurrenciesAsync()
        {
            var path = $"{BASE_URL}{SYMBOLS_URL}{KEY_URL}{settings.ApiKey}";

            var client = this.clientFactory.CreateClient();

            FixerCurrenciesDto currencies = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                currencies = JsonConvert.DeserializeObject<FixerCurrenciesDto>(json);
            }

            return currencies;
        }

        public async Task<FixerExchangeRateDto> GetLatestRateForEuroAsync()
        {
            var path = $"{BASE_URL}{LATEST_URL}{KEY_URL}{settings.ApiKey}";

            var client = this.clientFactory.CreateClient();

            FixerExchangeRateDto rate = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                rate = JsonConvert.DeserializeObject<FixerExchangeRateDto>(json);
            }

            return rate;
        }

        public async Task<FixerExchangeRateDto> GetHistoricalRateForEuroAsync(string date)
        {
            var path = $"{BASE_URL}{date}{KEY_URL}{settings.ApiKey}";

            var client = this.clientFactory.CreateClient();

            FixerExchangeRateDto rate = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                rate = JsonConvert.DeserializeObject<FixerExchangeRateDto>(json);
            }

            return rate;
        }
    }
}