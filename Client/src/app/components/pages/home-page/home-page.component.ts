import { Component } from '@angular/core';
import {ICurrentWeather} from "../../../models/ICurrentWeather";
import {WeatherService} from "../../../services/weather.service";
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {ICurrentAirPollution} from "../../../models/ICurrentAirPollution";
import {CurrentWeatherCardComponent} from "../../partials/current-weather-card/current-weather-card.component";
import {
  CurrentWeatherDetailsCardComponent
} from "../../partials/current-weather-details-card/current-weather-details-card.component";
import {FiveDaysForecastComponent} from "../../partials/five-days-forecast/five-days-forecast.component";
import {HourlyWeatherCardComponent} from "../../partials/hourly-weather-card/hourly-weather-card.component";

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    DatePipe,
    NgForOf,
    CurrentWeatherCardComponent,
    CurrentWeatherDetailsCardComponent,
    FiveDaysForecastComponent,
    HourlyWeatherCardComponent,
    NgIf
  ],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {
  currentWeather: ICurrentWeather | null = null;
  airPollution: ICurrentAirPollution | null = null;
  hourlyWeatherToday: any[] = [];
  dailyHighestTemp: any[] = [];
  constructor(private weatherService: WeatherService) {}

  ngOnInit() {
    this.weatherService.weather$.subscribe(weather => {
      if (weather?.currentWeather) {
        this.updateWeather(weather.currentWeather);
      }
      if (weather?.dailyHighestTemp) {
        this.dailyHighestTemp = weather.dailyHighestTemp;
      }
      if (weather?.hourlyWeatherToday) {
        this.hourlyWeatherToday = weather.hourlyWeatherToday;
      }
    });
  }
  getCurrentAirPollution() {
    if (this.currentWeather) {
      const lat = this.currentWeather.coord.lat;
      const lon = this.currentWeather.coord.lon;
      this.weatherService.getCurrentAirPollution(lat, lon).subscribe({
        next: (response) => {
          this.airPollution = response;
        },
        error: (error) => console.error("There was an error!", error)
      });
    }
  }
  updateWeather(weather: ICurrentWeather) {
    this.currentWeather = weather;
    this.getCurrentAirPollution();
  }
}
