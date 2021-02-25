import { Component, OnInit } from '@angular/core';
import { ICurrency } from '../interfaces/currency';
import { IExchangeHistoricalDetails, IExchangeLatestDetails } from '../interfaces/exchangeDetails';
import { ApiServiceService } from '../services/api-service.service';

@Component({
  selector: 'app-exchange-calculator',
  templateUrl: './exchange-calculator.component.html',
  styleUrls: ['./exchange-calculator.component.css']
})
export class ExchangeCalculatorComponent implements OnInit {

  availableCurrencies: ICurrency[] = [];
  isLoading = false;
  choiseMade = false;
  isHistorical = false;
  gotResult = false;
  result = 0;

  constructor(private apiService: ApiServiceService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.apiService.getCurrencies().subscribe(currencies => {
      this.availableCurrencies = currencies;
      this.isLoading = false;
    });
  }

  chooseLatest(): void {
    this.choiseMade = true;
    this.isHistorical = false;
  }

  chooseHistorical(): void {
    this.choiseMade = true;
    this.isHistorical = true;
  }

  exchangeLatest(formValue: IExchangeLatestDetails): void {

  }

  exchangeHistorical(formValue: IExchangeHistoricalDetails): void {

  }

}
