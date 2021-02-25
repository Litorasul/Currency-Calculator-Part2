import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExchangeCalculatorComponent } from './exchange-calculator.component';

describe('ExchangeCalculatorComponent', () => {
  let component: ExchangeCalculatorComponent;
  let fixture: ComponentFixture<ExchangeCalculatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExchangeCalculatorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExchangeCalculatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
