import { Component } from '@angular/core';
import {ICurrentWeather} from "../../../models/ICurrentWeather";
import {WeatherService} from "../../../services/weather.service";
import {DatePipe} from "@angular/common";
import {ICurrentAirPollution} from "../../../models/ICurrentAirPollution";

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    DatePipe
  ],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {
  currentWeather?: ICurrentWeather;
  airPollution?: ICurrentAirPollution;
  roundedTemp?: number;
  sunrise?: Date;
  sunset?: Date;
  exampleLat:number = 52.229675;
  exampleLon:number = 21.012230;

  constructor(private weatherService:WeatherService) {}

  ngOnInit(){
    this.getCurrentWeather();
    this.getCurrentAirPollution();
  }

  getCurrentWeather() {
    this.weatherService.getCurrentWeather(this.exampleLat, this.exampleLon).subscribe({
      next: (response) => {
        this.currentWeather = response;
        this.roundedTemp = Math.round(this.currentWeather!.main!.temp);
        this.sunrise = new Date(this.currentWeather!.sys!.sunrise * 1000);
        this.sunset = new Date(this.currentWeather!.sys!.sunset * 1000);
      },
      error: (error) => console.error("There was an error!", error)
    });
  }

  getCurrentAirPollution() {
    this.weatherService.getCurrentAirPollution(this.exampleLat, this.exampleLon).subscribe({
      next: (response) => {
        this.airPollution = response;
      },
      error: (error) => console.error("There was an error!", error)
    })
  }
}
