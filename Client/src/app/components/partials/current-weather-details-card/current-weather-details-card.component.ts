import {Component, Input, SimpleChanges} from '@angular/core';
import {ICurrentWeather} from "../../../models/ICurrentWeather";
import {ICurrentAirPollution} from "../../../models/ICurrentAirPollution";
import {DatePipe, NgIf} from "@angular/common";

@Component({
  selector: 'app-current-weather-details-card',
  standalone: true,
  imports: [
    DatePipe,
    NgIf
  ],
  templateUrl: './current-weather-details-card.component.html',
  styleUrl: './current-weather-details-card.component.scss'
})
export class CurrentWeatherDetailsCardComponent {
  @Input() currentWeather: ICurrentWeather | null = null;
  @Input() airPollution: ICurrentAirPollution | null = null;
  sunrise?: Date;
  sunset?: Date;
  airQualityLabel: string = 'Unknown';

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['currentWeather'] || changes['airPollution']) {
      if (this.currentWeather) {
        this.sunrise = new Date(this.currentWeather.sys.sunrise * 1000);
        this.sunset = new Date(this.currentWeather.sys.sunset * 1000);
      }
      if (this.airPollution) {
        this.updateAirQualityLabel();
      }
    }
  }

  private updateAirQualityLabel() {
    if (this.airPollution) {
      const aqi = this.airPollution.list[0].main.aqi;
      switch (aqi) {
        case 1: this.airQualityLabel = 'Good'; break;
        case 2: this.airQualityLabel = 'Fair'; break;
        case 3: this.airQualityLabel = 'Moderate'; break;
        case 4: this.airQualityLabel = 'Poor'; break;
        case 5: this.airQualityLabel = 'Very Poor'; break;
        default: this.airQualityLabel = 'Unknown';
      }
    }
  }
}
