import { useState } from 'react';
import cls from './WeatherMeasurementForm.module.css';
import { Button } from '../../UI/Button/Button';
import { WeatherMeasurement } from 'models/Measurement';
import { API_BASE_URL } from '../../../constants/constants';

export function WeatherMeasurementForm() {
  const [date, setDate] = useState<string>('');
  const [partOfDay, setPartOfDay] = useState<string>('0');
  const [precipitationType, setPrecipitationType] = useState<string>('0');
  const [windDirection, setWindDirection] = useState<string>('0');
  const [temperature, setTemperature] = useState<string>('0');
  const [pressure, setPressure] = useState<string>('740');
  const [windSpeed, setWindSpeed] = useState<string>('0');

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

    const measurementData: WeatherMeasurement = {
      date: Date.parse(date) / 1000,
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
            onChange={(event) => setPartOfDay(event.currentTarget.value)}
          >
            <option value="1">Утро</option>
            <option value="2">День</option>
            <option value="3">Вечер</option>
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
            <option value="1">Облачно</option>
            <option value="2">Солнечно</option>
            <option value="3">Дождь</option>
            <option value="4">Снег</option>
            <option value="5">Снег с дождем</option>
            <option value="6">Облачно с прояснениями</option>
          </select>
        </div>
        <div className={cls.selectWrapper}>
          <label htmlFor="wind_direction">Направление ветра</label>
          <select
            onChange={(event) => setWindDirection(event.currentTarget.value)}
            name="wind_direction"
            id="wind_direction"
          >
            <option value="1">Южный</option>
            <option value="2">Северный</option>
            <option value="3">Западный</option>
            <option value="4">Восточный</option>
            <option value="5">Юго-западный</option>
            <option value="6">Юго-восточный</option>
            <option value="7">Северо-западный</option>
            <option value="8">Северо-восточный</option>
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
