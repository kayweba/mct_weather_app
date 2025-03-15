import { useEffect, useState } from 'react';

import { LeftPanel } from './components/LeftPanel/LeftPanel';
import { WeatherData } from './components/WeatherData/WeatherData';
import { WeatherMeasurementOfDay } from './models/Measurement';
import { weatherService } from './services/weatherService/weatherService';
import './App.css';
import { getReadableDateFromTimestamp } from './utils/getReadableDate';
import { getAverageTemperature } from './utils/getAverageTemperature';
import { LOG } from './common/Logger';

function App() {
  const [weatherData, setWeatherData] = useState<WeatherMeasurementOfDay[]>([]);
  const [filteredData, setFilterData] = useState<WeatherMeasurementOfDay[]>([]);

  const getMeasurements = () => {
    const makeRequest = async () => {
      weatherService
        .getAllMeasurements()
        .then(async (data) => {
          if (data.status === 200) {
            const json = await data.json() as WeatherMeasurementOfDay[];
            const sortedByDateDesc = json.sort((a, b) => b.date - a.date)
            setWeatherData(sortedByDateDesc);
            setFilterData(sortedByDateDesc);
            LOG.V('Запрос на получение списка измерений успешно выполнен', json)
          }
          if (data.status === 404) {
            LOG.E(`Неверно указан адрес или порт: ${import.meta.env.VITE_BASE_URL}`)
            alert(
              'Неверно указан адрес или порт подключения к серверу. Проверьте параметры конфигурации и попробуйте снова!'
            );
          }
        })
        .catch((error) => {
          LOG.E(`Произошла ошибка при запросе из сервиса weatherService.getAllMeasurements()\n${error}`)
          alert(
            `Ошибка при загрузке данных! Проверьте, верно ли указан адрес и порт сервера, а также состояние самого сервера!\n${error}`
          );
        });
    };
    makeRequest();
  };

  const searchFn = (
    value: string,
    by: 'date' | 'morning' | 'afternoon' | 'evening' | 'average'
  ) => {
    switch (by) {
      case 'date': {
        const filtered = weatherData.filter((item) =>
          getReadableDateFromTimestamp(item.date).includes(value)
        );
        setFilterData(filtered);
        break;
      }
      case 'morning': {
        const filtered = weatherData.filter((item) =>
          String(item.morning_temperature).includes(value)
        );
        setFilterData(filtered);
        break;
      }
      case 'afternoon':
        {
          const filtered = weatherData.filter((item) =>
            String(item.afternoon_temperature).includes(value)
          );
          setFilterData(filtered);
        }
        break;
      case 'evening':
        {
          const filtered = weatherData.filter((item) =>
            String(item.evening_temperature).includes(value)
          );
          setFilterData(filtered);
        }
        break;
      case 'average':
        {
          const filtered = weatherData.filter((item) => {
            const avg = getAverageTemperature([
              item.morning_temperature,
              item.afternoon_temperature,
              item.evening_temperature,
            ]);
            return String(avg).includes(value);
          });
          setFilterData(filtered);
        }
        break;
      default:
        break;
    }
  };

  useEffect(() => {
    getMeasurements();
  }, []);

  return (
    <div className={'container'}>
      <LeftPanel getMeasurements={getMeasurements} />
      <WeatherData data={filteredData} search={searchFn} />
    </div>
  );
}

export default App;
