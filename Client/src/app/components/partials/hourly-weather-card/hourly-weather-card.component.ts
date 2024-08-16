import {Component, Input} from '@angular/core';
import {DatePipe, NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-hourly-weather-card',
  standalone: true,
  imports: [
    DatePipe,
    NgIf,
    NgForOf
  ],
  templateUrl: './hourly-weather-card.component.html',
  styleUrl: './hourly-weather-card.component.scss'
})
export class HourlyWeatherCardComponent {
  @Input() hourlyWeatherToday: any[] = [];
}
