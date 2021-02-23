namespace CurrencyCalculatorApi.Data
{
    using Microsoft.EntityFrameworkCore;

    using CurrencyCalculatorApi.Models;
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
        {
            
        }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }
    }
}