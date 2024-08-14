import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ICurrentWeather} from "../models/ICurrentWeather";
import {Observable} from "rxjs";
import {ICurrentAirPollution} from "../models/ICurrentAirPollution";

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  baseUrl = "https://localhost:7206/";
  constructor(private http: HttpClient) { }

  getCurrentWeather(lat: number, lon: number): Observable<any> {
    return this.http.get<ICurrentWeather>(`${this.baseUrl}Weather/current?lat=${lat}&lon=${lon}`);
  }

  getCurrentWeatherByCity(cityName: string): Observable<any> {
    return this.http.get(`${this.baseUrl}Weather/currentByCity?cityName=${cityName}`);
  }

  // AirPollution
  getCurrentAirPollution(lat: number, lon: number): Observable<any> {
    return this.http.get<ICurrentAirPollution>(`${this.baseUrl}Weather/currentAirPollution?lat=${lat}&lon=${lon}`)
  }
}
