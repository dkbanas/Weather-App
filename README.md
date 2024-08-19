# ⛅ Weather App
> *This application is my project made in the process of learning .NET and Angular*

## ℹ️ Overview

MyWeather is a web application designed to offer comprehensive weather information. Built with Angular 18.1.4 for the frontend and .NET 8.0 for the backend, it integrates with the OpenWeather API to deliver real-time weather data, air pollution details, and forecasts.Whether you need current weather conditions, a five-day forecast, or hourly updates, MyWeather provides all the essential features.

## 🌟 Key features
- Current Weather: View real-time temperature, humidity, and weather conditions for any city
- Air Pollution: Access detailed information on air quality and pollution levels.
- 5-Day Forecast: Get a detailed weather forecast for the next five days
- Hourly Weather: Check hourly weather data for today to plan your day effectively.

## 🏗️ Patterns
 - Clean Architecture - way to structure your code and to separate the concerns of the application into layers. The main idea is to separate the business logic from the infrastructure and presentation layers. This way, the core domain business logic and rules are at the core layer of the application, external services are implemented at the infrastructure layer, and the presentation layer is the entry point of the application.

## 🧰 Used NuGet Addons:
- Microsoft.Extensions.Configuration v8.0.0
- Microsoft.Extensions.Logging.Abstractions v8.0.1

## ⚙️ Technologies Used:
- Frontend: Angular 18.1.4
- Backend: .NET 8.0
- Weather Data: OpenWeather API (Free Plan)
- Styling: Bootstrap 5 for modern and responsive design

## 🛠️ How to run
- 1.Clone repository
- 2.Open appsettings.Development.json file inside API folder and fill your "ApiKey"
- 3.Click run in your IDE to run backend.
- 4.Right click in the Client folder, open in terminal and write
```CMD
>>>PS D:\Weather\Client> ng serve
```
