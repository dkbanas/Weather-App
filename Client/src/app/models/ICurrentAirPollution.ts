﻿export interface ICurrentAirPollution {
  coord: Coord
  list: List[]
}

export interface Coord {
  lon: number
  lat: number
}

export interface List {
  dt: number
  main: Main
  components: Components
}

export interface Main {
  aqi: number
}

export interface Components {
  co: number
  no: number
  no2: number
  o3: number
  so2: number
  pm2_5: number
  pm10: number
  nh3: number
}
