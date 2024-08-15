import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ICurrentWeather} from "../models/ICurrentWeather";
import {BehaviorSubject, Observable} from "rxjs";
import {ICurrentAirPollution} from "../models/ICurrentAirPollution";
import {IForecast} from "../models/IForecast";

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  baseUrl = "https://localhost:7206/";
  private weatherSubject = new BehaviorSubject<any>(null);
  weather$ = this.weatherSubject.asObservable();
  constructor(private http: HttpClient) { }

  updateWeather(weather: any) {
    this.weatherSubject.next(weather);
  }

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

  //Weather For Next Days
  getWeatherForNextDays(lat:number, lon:number): Observable<any> {
    return this.http.get<IForecast>(`${this.baseUrl}Weather/prediction?lat=${lat}&lon=${lon}`)
  }
  //Suggestion
  getPlaceSuggestions(query: string): Observable<any[]> {
    const url = `${this.baseUrl}Weather/places?query=${query}`;
    return this.http.get<any[]>(url);
  }

  // getWeatherByCity(cityName: string): Observable<any> {
  //   const url = `${this.baseUrl}/currentByCity?cityName=${cityName}`;
  //   return this.http.get<any>(url);
  // }


}
