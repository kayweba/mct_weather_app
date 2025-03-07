import { useState } from 'react';
import cls from './WeatherMeasurementForm.module.css';
import { Button } from '../../UI/Button/Button';
import { PartOfDay, PrecipitationType, WeatherMeasurement, WindDirection } from '../../../models/Measurement';
import { API_BASE_URL } from '../../../constants/constants';

export function WeatherMeasurementForm() {
  const [date, setDate] = useState<string>('');
  const [partOfDay, setPartOfDay] = useState<PartOfDay>(PartOfDay.MORNING);
  const [precipitationType, setPrecipitationType] = useState<PrecipitationType>(PrecipitationType.CLOUD);
  const [windDirection, setWindDirection] = useState<WindDirection>(WindDirection.SOUTH);
  const [temperature, setTemperature] = useState<number>(0);
  const [pressure, setPressure] = useState<number>(740);
  const [windSpeed, setWindSpeed] = useState<number>(0);

  // const [error, setError] = useState<string[]>([])

  // TODO: Переписать валидацию!
  const checkIsValid = () => {

    let isValid: boolean = true;

    [
      date,
      partOfDay,
      precipitationType,
      windDirection,
      temperature,
      pressure,
      windSpeed,
    ].forEach(parameter => {
      if (parameter === null || parameter === '') {
        isValid = false
      }
    })
    return isValid
  };

  const sendWeatherData = async (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>
  ) => {
    event.preventDefault();
    console.log(
      date,
      partOfDay,
      precipitationType,
      windDirection,
      temperature,
      pressure,
      windSpeed
    );

    const isValid = checkIsValid();
    if (!isValid) {
      alert('Не все поля формы заполнены!');
      return
    }

    const getUTCTimestamp = () => {
      return new Date(Date.parse(date)).getTime() / 1000
    }

    const measurementData: WeatherMeasurement = {
      date: getUTCTimestamp(),
      part_of_day: Number(partOfDay),
      precipitation_type: Number(precipitationType),
      temperature: Number(temperature),
      pressure: Number(pressure),
      wind_speed: Number(windSpeed),
      wind_direction: Number(windDirection),
      force_overwrite: false,
    }

    console.log(measurementData)

    const response = await fetch(`${API_BASE_URL}/measures`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json;'
      },
      body: JSON.stringify(measurementData)
    })

    const jsonData = await response.json()
    console.log(jsonData)
  };

  return (
    <form action="#">
      <div className={cls.container}>
        <div>
          <p>Дата измерения</p>
          <input
            type="date"
            className={cls.date}
            value={date}
            onChange={(event) => setDate(event.currentTarget.value)}
          />
        </div>
        <div className={cls.selectWrapper}>
          <label htmlFor="part_of_day">Часть дня</label>
          <select
            name="part_of_day"
            id="part_of_day"
            value={partOfDay}
            onChange={(event) => setPartOfDay(Number(event.currentTarget.value))}
          >
            <option value={PartOfDay.MORNING}>Утро</option>
            <option value={PartOfDay.AFTERNOON}>День</option>
            <option value={PartOfDay.EVENING}>Вечер</option>
          </select>
        </div>
        <div className={cls.selectWrapper}>
          <label htmlFor="precipitation_type">Тип осадков</label>
          <select
            name="precipitation_type"
            id="precipitation_type"
            value={precipitationType}
            onChange={(event) =>
              setPrecipitationType(Number(event.currentTarget.value))
            }
          >
            <option value={PrecipitationType.CLOUD}>Облачно</option>
            <option value={PrecipitationType.SUN}>Солнечно</option>
            <option value={PrecipitationType.RAIN}>Дождь</option>
            <option value={PrecipitationType.SNOW}>Снег</option>
            <option value={PrecipitationType.SNOW_WITH_RAIN}>Снег с дождем</option>
            <option value={PrecipitationType.PARTLY_CLOUDY}>Облачно с прояснениями</option>
          </select>
        </div>
        <div className={cls.selectWrapper}>
          <label htmlFor="wind_direction">Направление ветра</label>
          <select
            onChange={(event) => setWindDirection(Number(event.currentTarget.value))}
            name="wind_direction"
            id="wind_direction"
          >
            <option value={WindDirection.SOUTH}>Южный</option>
            <option value={WindDirection.NORTHERN}>Северный</option>
            <option value={WindDirection.WESTERN}>Западный</option>
            <option value={WindDirection.EASTERN}>Восточный</option>
            <option value={WindDirection.SOUTHWEST}>Юго-западный</option>
            <option value={WindDirection.SOUTHEAST}>Юго-восточный</option>
            <option value={WindDirection.NORTHWEST}>Северо-западный</option>
            <option value={WindDirection.NORTHEAST}>Северо-восточный</option>
          </select>
        </div>
        <div>
          <p>Температура &deg;C</p>
          <input
            type="number"
            value={temperature}
            onChange={(event) => setTemperature(Number(event.currentTarget.value))}
          />
        </div>
        <div>
          <p>Давление</p>
          <input
            type="number"
            value={pressure}
            onChange={(event) => setPressure(Number(event.currentTarget.value))}
          />
        </div>
        <div>
          <p>Скорость ветра (м/c)</p>
          <input
            type="number"
            value={windSpeed}
            onChange={(event) => setWindSpeed(Number(event.currentTarget.value))}
          />
        </div>
        <div>
          <Button onClick={sendWeatherData}>Записать измерения</Button>
        </div>
      </div>
    </form>
  );
}
