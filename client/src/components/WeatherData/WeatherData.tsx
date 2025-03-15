import { WeatherMeasurementOfDay } from 'models/Measurement';
import { PrecipitationIcon } from './PrecipitationIcon';
import { WindDirectionIcon } from './WindDirectionIcon';
import cls from './WeatherData.module.css';
import { Icons } from '../Icons';
import { getReadableDateFromTimestamp } from '../../utils/getReadableDate';
import { useState } from 'react';
import { getAverageTemperature } from '../../utils/getAverageTemperature';

type Props = {
  data: WeatherMeasurementOfDay[];
  search(value: string, by: string): void;
};

export function WeatherData({ data, search }: Props) {
  const [isShowDateSearchInput, setIsShowSearchInput] =
    useState<boolean>(false);
  const [searchByDateInputValue, setSearchByDateInputValue] =
    useState<string>('');

  const [isShowSearchMorningInput, setIsShowSearchMorningInput] =
    useState<boolean>(false);
  const [searchByMorningInputValue, setSearchByMorningInputValue] =
    useState<string>('');

  const [isShowSearchAfternoonInput, setIsShowSearchAfternoonInput] =
    useState<boolean>(false);
  const [searchByAfternoonInputValue, setSearchByAfternoonInputValue] =
    useState<string>('');

  const [isShowSearchEveningInput, setIsShowSearchEveningInput] =
    useState<boolean>(false);
  const [searchEveningInputValue, setSearchEveningInputValue] =
    useState<string>('');

  const [isShowSearchAverageInput, setIsShowSearchAverageInput] = useState<boolean>(false)
  const [searchAverageInputValue, setSearchAverageInputValue] = useState<string>('')

  const onChangeSearchByDateInputValue = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setSearchByDateInputValue(event.currentTarget.value);
    search(event.currentTarget.value, 'date');
  };

  const onChangeSearchByMorningInputValue = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setSearchByMorningInputValue(event.currentTarget.value);
    search(event.currentTarget.value, 'morning');
  };

  const onChangeSearchByAfternoonInputValue = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setSearchByAfternoonInputValue(event.currentTarget.value);
    search(event.currentTarget.value, 'afternoon');
  };

  const onChangeSearchByEveningInputValue = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setSearchEveningInputValue(event.currentTarget.value);
    search(event.currentTarget.value, 'evening');
  };

  const onChangeSearchByAverageInputValue = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setSearchAverageInputValue(event.currentTarget.value)
    search(event.currentTarget.value, 'average')
  }

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
            <th>
              <div className={cls.thWithIcon}>
                <p>Утро</p>
                <Icons.Search
                  className={cls.thIcon}
                  onClick={() =>
                    setIsShowSearchMorningInput(!isShowSearchMorningInput)
                  }
                />
              </div>
              {isShowSearchMorningInput && (
                <input
                  type="text"
                  className={cls.searchInput}
                  value={searchByMorningInputValue}
                  onChange={onChangeSearchByMorningInputValue}
                  placeholder="C&deg;"
                />
              )}
            </th>
            <th>
              <div className={cls.thWithIcon}>
                <p>День</p>
                <Icons.Search
                  className={cls.thIcon}
                  onClick={() =>
                    setIsShowSearchAfternoonInput(!isShowSearchAfternoonInput)
                  }
                />
              </div>
              {isShowSearchAfternoonInput && (
                <input
                  type="text"
                  className={cls.searchInput}
                  value={searchByAfternoonInputValue}
                  onChange={onChangeSearchByAfternoonInputValue}
                  placeholder="C&deg;"
                />
              )}
            </th>
            <th>
              <div className={cls.thWithIcon}>
                <p>Вечер</p>
                <Icons.Search
                  className={cls.thIcon}
                  onClick={() =>
                    setIsShowSearchEveningInput(!isShowSearchEveningInput)
                  }
                />
              </div>
              {isShowSearchEveningInput && (
                <input
                  type="text"
                  className={cls.searchInput}
                  value={searchEveningInputValue}
                  onChange={onChangeSearchByEveningInputValue}
                  placeholder="C&deg;"
                />
              )}
            </th>
            <th>
              <div className={cls.thWithIcon}>
                <p>Среднее</p>
                <Icons.Search
                  className={cls.thIcon}
                  onClick={() =>
                    setIsShowSearchAverageInput(!isShowSearchAverageInput)
                  }
                />
              </div>
              {isShowSearchAverageInput && (
                <input
                  type="text"
                  className={cls.searchInput}
                  value={searchAverageInputValue}
                  onChange={onChangeSearchByAverageInputValue}
                  placeholder="C&deg;"
                />
              )}
            </th>
          </tr>
        </thead>
        <tbody>
          {data.map((day, index) => {
            return (
              <tr key={index}>
                <td>{getReadableDateFromTimestamp(day.date)}</td>
                <td className={cls.tdWrapper}>
                  {day.morning_temperature !== null &&
                    <div className={cls.temperature}>
                      <p>{day.morning_temperature}&deg;</p>
                      {PrecipitationIcon(day.morning_precipitation_type)}
                    </div>
                  }
                  {day.morning_wind_speed !== null &&
                    <div className={cls.wind}>
                      <p>{day.morning_wind_speed} м/с</p>
                      {WindDirectionIcon(day.morning_wind_direction)}
                    </div>
                  }
                  {
                    day.morning_pressure !== null &&
                      <div className={cls.pressure}>
                        <p>{day.morning_pressure} мм рт. ст.</p>
                      </div>
                  }
                </td>
                <td>
                  {
                    day.afternoon_temperature !== null &&
                    <div className={cls.temperature}>
                      <p>{day.afternoon_temperature}&deg;</p>
                      {PrecipitationIcon(day.afternoon_precipitation_type)}
                    </div>
                  }
                  {
                    day.afternoon_wind_speed !== null &&
                    <div className={cls.wind}>
                      <p>{day.afternoon_wind_speed} м/с</p>
                      {WindDirectionIcon(day.afternoon_wind_direction)}
                    </div>
                  }
                  {
                    day.afternoon_pressure !== null &&
                    <div className={cls.pressure}>
                      <p>{day.afternoon_pressure} мм рт. ст.</p>
                    </div>
                  }
                </td>
                <td>
                  {
                    day.evening_temperature !== null &&
                    <div className={cls.temperature}>
                      <p>{day.evening_temperature}&deg;</p>
                      {PrecipitationIcon(day.evening_precipitation_type)}
                    </div>
                  }
                  {
                    day.evening_wind_speed !== null &&
                    <div className={cls.wind}>
                      <p>{day.evening_wind_speed} м/с</p>
                      {WindDirectionIcon(day.evening_wind_direction)}
                    </div>
                  }
                  {
                    day.evening_pressure !== null &&
                    <div className={cls.pressure}>
                      <p>{day.evening_pressure} мм рт. ст.</p>
                    </div>
                  }
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
