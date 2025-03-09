import { WeatherMeasurementOfDay } from 'models/Measurement';
import { PrecipitationIcon } from './PrecipitationIcon';
import { WindDirectionIcon } from './WindDirectionIcon';
import cls from './WeatherData.module.css';
import { Icons } from '../Icons';
import { getReadableDateFromTimestamp } from '../../utils/getReadableDate';
import { useState } from 'react';

type Props = {
  data: WeatherMeasurementOfDay[];
  search(value: string, by: string): void
};

export function WeatherData({ data, search }: Props) {
  const [isShowDateSearchInput, setIsShowSearchInput] =
    useState<boolean>(false);
  const [searchByDateInputValue, setSearchByDateInputValue] =
    useState<string>('');

  const onChangeSearchByDateInputValue = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearchByDateInputValue(event.currentTarget.value)
    search(event.currentTarget.value, 'date')
  }

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

  // TODO: Добавить проверку на null, чтобы не выводить лишние символы.
  return (
    <div className={cls.wrapper}>
      <table className={cls.table}>
        <thead>
          <tr>
            <th>
              <div className={cls.thWithIcon}>
                <p>Дата</p>
                <Icons.Search
                  className={cls.thIcon}
                  onClick={() => setIsShowSearchInput(!isShowDateSearchInput)}
                />
              </div>
              {isShowDateSearchInput && (
                <input
                  type="text"
                  className={cls.searchInput}
                  value={searchByDateInputValue}
                  onChange={onChangeSearchByDateInputValue}
                />
              )}
            </th>
            <th>Утро</th>
            <th>День</th>
            <th>Вечер</th>
            <th>Среднее</th>
          </tr>
        </thead>
        <tbody>
          {data.map((day, index) => {
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
