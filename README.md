# Currency-Calculator-Part2

A simple Currency Calculator with the opportunity to check the latest or historical exchange rates according to fixer.io.
It also retrieves the latest exchange rates onse a day and saves it in a SQL Server Database and gives the user the option to check how an exchange rate for any currency to Euro develop for a time period with a Linear Chart.

### The project consists of two parts:
1. ASP.NET Core 5 Web API Server
2. Angular 11.0.1 Web Client

## How to run

Clone or download.

### Server:

1. Modify the [Connection String](https://github.com/Litorasul/Currency-Calculator-Part2/blob/main/Server/CurrencyCalculatorApi/CurrencyCalculatorApi/appsettings.json) to reflect your database environment.
2. Open [fixer.io](https://fixer.io/), create an account and take your free API KEY.
3. Type your API Key in [the FixerSettings class](https://github.com/Litorasul/Currency-Calculator-Part2/blob/main/Server/CurrencyCalculatorApi/CurrencyCalculatorApi/Common/FixerSettings.cs).
4. Build and Run.

### Client:

Run `ng serve` and navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.