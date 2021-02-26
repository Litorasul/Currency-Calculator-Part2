import { Component, OnInit } from '@angular/core';

import { ApiServiceService } from '../services/api-service.service';

import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Label, Color } from 'ng2-charts';
import { IExchangeRate } from '../interfaces/exchangeRate';
import { ICurrency } from '../interfaces/currency';
import { ICurrencyCode } from '../interfaces/currencyCode';


@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {

  errorMessage = '';
  initialized = false;
  availableCurrencies: ICurrency[] = [];
  isLoading = false;

  lineChartData: ChartDataSets[] = [];
  lineChartLabels: Label[] = [];
  lineChartType: ChartType = 'line';
  lineChartColors: Color[] = [

    { // green
      backgroundColor: 'rgba(73,210,169,0.2)',
      borderColor: 'rgba(73,210,169,1)',
    }
  ];
  lineChartOptions: ChartOptions = {
    responsive: true
  };

  constructor(private apiService: ApiServiceService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.apiService.getCurrencies().subscribe(currencies => {
      this.availableCurrencies = currencies;
      this.isLoading = false;
    });
  }

  getChartData(formValue: ICurrencyCode): void {
    this.apiService.getExchangeRatesPerCurrency(formValue.code).subscribe(
      {
        next: (data: IExchangeRate[]) => {
          const dataSet: ChartDataSets = {};
          this.lineChartLabels = [];
          dataSet.label = formValue.code;
          dataSet.data = [];
          data.forEach(element => {
            dataSet.data?.push(element.rate);
            this.lineChartLabels.push(element.dateTime);
          });
          this.lineChartData = [];
          this.lineChartData.push(dataSet);
          this.initialized = true;
        },
        error: (err) => {
          this.errorMessage = err.error.message;
          window.alert(this.errorMessage);
        }
      }
    );
  }

}
