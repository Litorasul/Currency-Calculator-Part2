namespace CurrencyCalculatorApi
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    using CurrencyCalculatorApi.Data;
    using CurrencyCalculatorApi.Common;
    using CurrencyCalculatorApi.Services;

    using Hangfire;
    using Hangfire.MemoryStorage;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CurrencyCalculatorApi", Version = "v1" });
            });

            services.AddHttpClient();

            // Hangfire Library for Scheduled tasks
            services.AddHangfire(config => 
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseDefaultTypeSerializer()
                    .UseMemoryStorage());
            services.AddHangfireServer();

            // Application Services
            services.AddTransient<IFixerService, FixerService>();
            services.AddTransient<ICalculatorService, CalculatorService>();
            services.AddTransient<IExchangeService, ExchangeService>();
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddTransient<IDailyTasksService, DailyTasksService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CurrencyCalculatorApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();
            // Run the task on start
            backgroundJobClient.Enqueue(() => serviceProvider.GetService<IDailyTasksService>().StoreLatestExchangeRateAsync());

            // Run the task daily
            recurringJobManager.AddOrUpdate(
                "Daily Exchange Rates",
                () => serviceProvider.GetService<IDailyTasksService>().StoreLatestExchangeRateAsync(),
                Cron.Daily);
        }
    }
}
