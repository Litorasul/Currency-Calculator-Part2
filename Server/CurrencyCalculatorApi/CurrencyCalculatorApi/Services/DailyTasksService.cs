namespace CurrencyCalculatorApi.Services
{
    using System.Threading.Tasks;

    public class DailyTasksService : IDailyTasksService
    {
        private readonly IDatabaseService dbService;
        private readonly IFixerService fixerService;

        public DailyTasksService(IDatabaseService dbService, IFixerService fixerService)
        {
            this.dbService = dbService;
            this.fixerService = fixerService;
        }

        public async Task StoreLatestExchangeRateAsync()
        {
            var rate = await this.fixerService.GetLatestRateForEuroAsync();
            if (!rate.Success)
            {
                return;
            }
            foreach (var item in rate.Rates)
            {
                await this.dbService.CreateExchangeRateAsync(item.Key, item.Value, rate.Date);
            }
        }
    }
}