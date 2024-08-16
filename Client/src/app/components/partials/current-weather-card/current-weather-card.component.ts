import {Component, Input} from '@angular/core';
import {ICurrentWeather} from "../../../models/ICurrentWeather";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-current-weather-card',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './current-weather-card.component.html',
  styleUrl: './current-weather-card.component.scss'
})
export class CurrentWeatherCardComponent {
  @Input() currentWeather: ICurrentWeather | null = null;

  get roundedTemp(): number {
    return Math.round(this.currentWeather?.main.temp || 0);
  }
}
