import {Component, Input} from '@angular/core';
import {DatePipe, NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-five-days-forecast',
  standalone: true,
  imports: [
    DatePipe,
    NgForOf,
    NgIf
  ],
  templateUrl: './five-days-forecast.component.html',
  styleUrl: './five-days-forecast.component.scss'
})
export class FiveDaysForecastComponent {
  @Input() dailyHighestTemp: any[] = [];
}
