import { Component } from '@angular/core';
import {WeatherService} from "../../../services/weather.service";
import {CommonModule} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {catchError, debounceTime, of, Subject, Subscription, switchMap} from "rxjs";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  searchQuery: string = '';
  suggestions: any[] = [];
  private searchSubject = new Subject<string>();
  private subscriptions: Subscription = new Subscription();

  constructor(private weatherService: WeatherService) {
    this.subscriptions.add(
      this.searchSubject.pipe(
        debounceTime(300),
        switchMap(query => {
          if (query.length < 3) {
            this.suggestions = [];
            return of([]);
          }
          return this.weatherService.getPlaceSuggestions(query);
        }),
        catchError(error => {
          console.error('Error fetching place suggestions:', error);
          this.suggestions = [];
          return of([]);
        })
      ).subscribe(data => {
        this.suggestions = data;
      })
    );
  }

  onSearch(): void {
    this.searchSubject.next(this.searchQuery);
  }

  onSearchButtonClick(): void {
    if (this.suggestions.length > 0) {
      this.selectSuggestion(this.suggestions[0]);
    }
  }

  selectSuggestion(suggestion: any): void {
    const {lat, lon, name} = suggestion;
    this.weatherService.getCurrentWeather(lat, lon).subscribe({
      next: (response) => {
        // Update weather including forecast
        this.weatherService.updateWeather({currentWeather: response, dailyHighestTemp: [], hourlyWeatherToday: []});

        // Fetch weather forecast for the selected location
        this.weatherService.getWeatherForNextDays(lat, lon).subscribe({
          next: (forecastResponse) => {
            this.weatherService.updateWeather({
              currentWeather: response,
              dailyHighestTemp: forecastResponse.dailyHighestTemp,
              hourlyWeatherToday: forecastResponse.hourlyWeatherToday
            });
          },
          error: (error) => console.error('Error fetching weather forecast:', error)
        });
      },
      error: (error) => console.error('Error fetching weather data:', error)
    });
    this.searchQuery = name;
    this.suggestions = [];
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

}
