import { PrecipitationIcon } from './PrecipitationIcon';
import { WindDirectionIcon } from './WindDirectionIcon';
import { Icons } from '../Icons';
import { mockWeatherMeasurementOfDays } from '../../mocks/WeatherMeasurementOfDays';
import { getReadableDateFromTimestamp } from '../../utils/getReadableDate';
import cls from './WeatherData.module.css';
import { useEffect } from 'react';

export function WeatherData() {
  const getAverageTemperature = (
    temperatures: Array<number | null>
  ): number | null => {
    const onlyExistsTemp = temperatures.filter(
      (temperature) => temperature !== null
    );
    const sumNum = onlyExistsTemp.reduce((acc, number) => acc + number, 0);
    if (onlyExistsTemp.length > 0) return sumNum / onlyExistsTemp.length;
    return null;
  };

  useEffect(() => {

    // const makeRequest = async () => {
    //   const response = await fetch('http://127.0.0.1:5117/weather', {
    //     mode: 'no-cors',
    //     method: "GET",
    //     headers: {
    //       'Content-Type': 'application/json;'
    //     }
    //   })
    //   console.log(response)
    // }
    // makeRequest()
  }, [])

  // TODO: Добавить проверку на null, чтобы не выводить лишние символы.
  return (
    <div className={cls.wrapper}>
      <table className={cls.table}>
        <thead>
          <tr>
            <th>Дата</th>
            <th>Утро</th>
            <th>День</th>
            <th>Вечер</th>
            <th>Среднее</th>
          </tr>
        </thead>
        <tbody>
          {mockWeatherMeasurementOfDays.map((day, index) => {
            return (
              <tr key={index}>
                <td>{getReadableDateFromTimestamp(day.date)}</td>
                <td className={cls.tdWrapper}>
                  <div className={cls.temperature}>
                    <p>{day.morning_temperature}&deg;</p>
                    {PrecipitationIcon(day.morning_precipitation_type)}
                  </div>
                  <div className={cls.wind}>
                    <p>{day.morning_wind_speed} м/с</p>
                    {WindDirectionIcon(day.morning_wind_direction)}
                  </div>
                  <div className={cls.pressure}>
                    <p>{day.morning_pressure} мм рт. ст.</p>
                  </div>
                </td>
                <td>
                  <div className={cls.temperature}>
                    <p>{day.afternoon_temperature}&deg;</p>
                    {PrecipitationIcon(day.afternoon_precipitation_type)}
                  </div>
                  <div className={cls.wind}>
                    <p>{day.afternoon_wind_speed} м/с</p>
                    {WindDirectionIcon(day.afternoon_wind_direction)}
                  </div>
                  <div className={cls.pressure}>
                    <p>{day.afternoon_pressure} мм рт. ст.</p>
                  </div>
                </td>
                <td>
                  <div className={cls.temperature}>
                    <p>{day.evening_temperature}&deg;</p>
                    {PrecipitationIcon(day.evening_precipitation_type)}
                  </div>
                  <div className={cls.wind}>
                    <p>{day.evening_wind_speed} м/с</p>
                    {WindDirectionIcon(day.evening_wind_direction)}
                  </div>
                  <div className={cls.pressure}>
                    <p>{day.evening_pressure} мм рт. ст.</p>
                  </div>
                </td>
                <td>
                  <div>
                    <Icons.Thermometer />
                  </div>
                  <p>
                    {getAverageTemperature([
                      day.morning_temperature,
                      day.afternoon_temperature,
                      day.evening_temperature,
                    ])?.toFixed(2)}
                    &deg;
                  </p>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}
