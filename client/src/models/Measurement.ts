

export type WeatherMeasurement = {
  date: number
  part_of_day: PartOfDay
  precipitation_type: PrecipitationType
  temperature: number
  pressure: number
  wind_speed: number
  wind_direction: WindDirection
  force_overwrite: boolean
}

export type WeatherMeasurementOfDay = {
  date: number
  morning_temperature: number | null;
  morning_pressure: number | null;
  morning_wind_speed: number | null;
  morning_wind_direction: WindDirection | null;
  morning_precipitation_type:  PrecipitationType | null;
  afternoon_temperature: number | null;
  afternoon_pressure: number | null;
  afternoon_wind_speed: number | null;
  afternoon_wind_direction: WindDirection | null;
  afternoon_precipitation_type:  PrecipitationType | null;
  evening_temperature: number | null;
  evening_pressure: number | null;
  evening_wind_speed: number | null;
  evening_wind_direction: WindDirection | null;
  evening_precipitation_type: PrecipitationType | null;
}


export enum PartOfDay {
  MORNING = 1,
  AFTERNOON = 2,
  EVENING = 3
}

export enum PrecipitationType {
  CLOUD = 1,
  SUN = 2,
  RAIN = 3,
  SNOW = 4,
  SNOW_WITH_RAIN = 5,
  PARTLY_CLOUDY = 6,
}

export enum WindDirection {
  SOUTH = 1,
  NORTHERN = 2,
  WESTERN = 3,
  EASTERN = 4,
  SOUTHWEST = 5,
  SOUTHEAST = 6,
  NORTHWEST = 7,
  NORTHEAST = 8,
}
