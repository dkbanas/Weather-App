import { Component } from '@angular/core';
import {WeatherService} from "../../../services/weather.service";
import {CommonModule} from "@angular/common";
import {FormsModule} from "@angular/forms";

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

  constructor(private weatherService: WeatherService) { }

  onSearch(): void {
    if (this.searchQuery.length < 3) {
      this.suggestions = [];
      return;
    }

    this.weatherService.getPlaceSuggestions(this.searchQuery).subscribe(
      data => {
        this.suggestions = data;
      },
      error => {
        console.error('Error fetching place suggestions:', error);
        this.suggestions = [];
      }
    );
  }

  onSearchButtonClick(): void {
    if (this.suggestions.length > 0) {
      this.selectSuggestion(this.suggestions[0]);
    }
  }

  selectSuggestion(suggestion: any): void {
    const { lat, lon, name } = suggestion;
    this.weatherService.getCurrentWeather(lat, lon).subscribe({
      next: (response) => {
        // Update weather including forecast
        this.weatherService.updateWeather({ currentWeather: response, dailyHighestTemp: [], hourlyWeatherToday: [] });

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

}
