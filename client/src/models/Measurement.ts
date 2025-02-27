

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

export enum PartOfDay {
  MORNING = 0,
  AFTERNOON = 1,
  EVENING = 2
}

export enum PrecipitationType {
  CLOUD = 0,
  SUN = 1,
  RAIN = 2,
  SNOW = 3,
  SNOW_WITH_RAIN = 4,
  PARTLY_CLOUDY = 5,
}

export enum WindDirection {
  SOUTH = 0,
  NORTHERN = 1,
  WESTERN = 2,
  EASTERN = 3,
  SOUTHWEST = 4,
  SOUTHEAST = 5,
  NORTHWEST = 6,
  NORTHEAST = 7,
}
