import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentWeatherDetailsCardComponent } from './current-weather-details-card.component';

describe('CurrentWeatherDetailsCardComponent', () => {
  let component: CurrentWeatherDetailsCardComponent;
  let fixture: ComponentFixture<CurrentWeatherDetailsCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CurrentWeatherDetailsCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CurrentWeatherDetailsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
