namespace CurrencyCalculatorApi.Services
{
    using System.Threading.Tasks;

    public interface IDailyTasksService
    {
        Task StoreLatestExchangeRateAsync();
    }
}