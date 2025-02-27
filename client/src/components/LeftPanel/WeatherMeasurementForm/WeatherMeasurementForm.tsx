import { useState } from 'react';
import { PartOfDay, WeatherMeasurement } from '../../../models/Measurement';
import cls from './WeatherMeasurementForm.module.css';
import { Button } from '../../UI/Button/Button';

const partOfDayByOptionValue: Record<string, PartOfDay> = {
  '0': 0,
  '1': 1,
  '2': 2,
};

export function WeatherMeasurementForm() {
  const [weatherData, setWeatherData] = useState<WeatherMeasurement | null>(
    null
  );

  const [date, setDate] = useState<string>('');
  const [partOfDay, setPartOfDay] = useState<string>('0');
  const [precipitationType, setPrecipitationType] = useState<string>('0');
  const [temperature, setTemperature] = useState<string>('0');
  const [pressure, setPressure] = useState<string>('740');
  const [windSpeed, setWindSpeed] = useState<string>('0');

  const sendWeatherData = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    event.preventDefault()
    console.log(date, partOfDay, precipitationType, temperature, pressure, windSpeed)
  }

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
            onChange={(event) => setPartOfDay(event.currentTarget.value)}
          >
            <option value="0">Утро</option>
            <option value="1">День</option>
            <option value="2">Вечер</option>
          </select>
        </div>
        <div className={cls.selectWrapper}>
          <label htmlFor="precipitation_type">Тип осадков</label>
          <select
            name="precipitation_type"
            id="precipitation_type"
            value={precipitationType}
            onChange={(event) =>
              setPrecipitationType(event.currentTarget.value)
            }
          >
            <option value="0">Облачно</option>
            <option value="1">Солнечно</option>
            <option value="2">Дождь</option>
            <option value="3">Снег</option>
            <option value="4">Снег с дождем</option>
            <option value="5">Облачно с прояснениями</option>
          </select>
        </div>
        <div className={cls.selectWrapper}>
          <label htmlFor="wind_direction">Направление ветра</label>
          <select name="wind_direction" id="wind_direction">
            <option value="0">Южный</option>
            <option value="1">Северный</option>
            <option value="2">Западный</option>
            <option value="3">Восточный</option>
            <option value="4">Юго-западный</option>
            <option value="5">Юго-восточный</option>
            <option value="6">Северо-западный</option>
            <option value="7">Северо-восточный</option>
          </select>
        </div>
        <div>
          <p>Температура &deg;C</p>
          <input
            type="number"
            value={temperature}
            onChange={(event) => setTemperature(event.currentTarget.value)}
          />
        </div>
        <div>
          <p>Давление</p>
          <input
            type="number"
            value={pressure}
            onChange={(event) => setPressure(event.currentTarget.value)}
          />
        </div>
        <div>
          <p>Скорость ветра (м/c)</p>
          <input
            type="number"
            value={windSpeed}
            onChange={(event) => setWindSpeed(event.currentTarget.value)}
          />
        </div>
        <div>
          <Button onClick={sendWeatherData}>Записать измерения</Button>
        </div>
      </div>
    </form>
  );
}
