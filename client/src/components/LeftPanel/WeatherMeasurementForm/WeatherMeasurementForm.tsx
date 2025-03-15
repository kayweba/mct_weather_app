import { useState } from 'react';
import cls from './WeatherMeasurementForm.module.css';
import { Button } from '../../UI/Button/Button';
import { PartOfDay, PrecipitationType, WeatherMeasurement, WindDirection } from '../../../models/Measurement';
import { weatherService } from '../../../services/weatherService/weatherService';

import { LOG } from '../../../common/Logger';

type Props = {
  getMeasurements: () => void
}

export function WeatherMeasurementForm({ getMeasurements }: Props) {
  const [date, setDate] = useState<string>('');
  const [partOfDay, setPartOfDay] = useState<PartOfDay>(PartOfDay.MORNING);
  const [precipitationType, setPrecipitationType] = useState<PrecipitationType>(PrecipitationType.CLOUD);
  const [windDirection, setWindDirection] = useState<WindDirection>(WindDirection.SOUTH);
  const [temperature, setTemperature] = useState<number>(0);
  const [pressure, setPressure] = useState<number>(740);
  const [windSpeed, setWindSpeed] = useState<number>(0);

  const getUTCTimestamp = () => {
    const utcTimestamp = new Date(Date.parse(date)).getTime() / 1000
    LOG.V(`Получение даты, введенной пользователем в формате UTC (timestamp) -> ${utcTimestamp}`)
    return utcTimestamp
  }

  const getUTCTodayTimestamp = () => {
    const todayUTCTimestamp = new Date().getTime() / 1000
    LOG.V(todayUTCTimestamp) 
    return todayUTCTimestamp
  }

  /*
    Форма устроена так, что пользователь может оставить пустой только дату.
    Этот случай и проверим.
   */
  const checkHasDate = () => {
    if (!date) {
      LOG.W('Для занесения измерения необходимо указать дату!')
      alert('Для занесения измерения необходимо указать дату!')
      return false
    }

    if (getUTCTimestamp() > getUTCTodayTimestamp()) {
      LOG.W('Выбранная дата должна быть меньше или равной сегодняшней!')
      alert('Выбранная дата должна быть меньше или равной сегодняшней!')
      return false
    }

    return true
  };


  const sendWeatherData = async (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>, rewrite: boolean = false
  ) => {
    event.preventDefault();

    const isValid = checkHasDate();
    // Если что-то невалидно - ничего не отправим.
    if (!isValid) return

    const measurementData: WeatherMeasurement = {
      date: getUTCTimestamp(),
      part_of_day: partOfDay,
      precipitation_type: precipitationType,
      temperature: temperature,
      pressure: pressure,
      wind_speed: windSpeed,
      wind_direction: windDirection,
      force_overwrite: rewrite,
    }
    try {
      const response = await weatherService.sendMeasurement(measurementData)
  
      if (response.status === 200) {
        const json = await response.json()
        LOG.V('Успешная отправка измерения, response -> ', json)
        getMeasurements()
      }
      if (response.status === 204) {
        LOG.W('Попытка записать данные за уже добавленный период')
        const needRewrite = confirm('В базе уже есть изменение за указанный период. Перезаписать? ')
  
        if (needRewrite) sendWeatherData(event, true) 
      }
    } catch (error) {
      alert('Произошла ошибка при попытке добавления измерения. Проверьте настройки сервера, доступность БД и попробуйте снова')
      LOG.E('Произошла ошибка при попытке добавления измерения -> ', error)
    }
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
            max={Date.now()}
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
