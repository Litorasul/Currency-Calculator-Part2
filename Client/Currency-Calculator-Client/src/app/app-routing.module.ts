import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChartComponent } from './chart/chart.component';
import { ExchangeCalculatorComponent } from './exchange-calculator/exchange-calculator.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/home'
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'calculator',
    component: ExchangeCalculatorComponent
  },
  {
    path: 'chart',
    component: ChartComponent
  }
];


export const AppRoutingModule = RouterModule.forRoot(routes);
