import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { ICurrency } from '../interfaces/currency';
import { IExchangeRate } from '../interfaces/exchangeRate';

// API URL Parts
const baseUrl = environment.apiBaseUrl;
const currenciesUrl = environment.apiCurrencies;
const exchangeUrl = environment.apiExchange;
const latestUrl = environment.apiLatest;
const historicalUrl = environment.apiHistorical;
const fromCurrencyUrl = environment.apiFrom;
const toCurrencyUrl = environment.apiTo;
const ammountUrl = environment.apiAmmount;
const dateUrl = environment.apiDate;
const exchangeRateUrl = environment.apiExchangeRate;
const currencyCodeUrl = environment.apiCurrencyCode;

@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {

  constructor(private http: HttpClient) { }

  getCurrencies(): Observable<ICurrency[]> {
    return this.http.get<ICurrency[]>(`${baseUrl}${currenciesUrl}`);
  }

  exchangeLatest(from: string, to: string, ammount: number): Observable<number> {
    return this.http.get<number>
    (`${baseUrl}${exchangeUrl}${latestUrl}${fromCurrencyUrl}${from}${toCurrencyUrl}${to}${ammountUrl}${ammount}`);
  }

  exchangeHistorical(from: string, to: string, ammount: number, date: string): Observable<number> {
    return this.http.get<number>
    (`${baseUrl}${exchangeUrl}${historicalUrl}${fromCurrencyUrl}${from}${toCurrencyUrl}${to}${ammountUrl}${ammount}${dateUrl}${date}`);
  }

  getExchangeRatesPerCurrency(currency: string): Observable<IExchangeRate[]> {
    return this.http.get<IExchangeRate[]>(`${baseUrl}${exchangeRateUrl}${currencyCodeUrl}${currency}`);
  }
}
