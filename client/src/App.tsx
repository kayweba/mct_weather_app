import { useEffect, useState } from 'react';

import { LeftPanel } from './components/LeftPanel/LeftPanel';
import { WeatherData } from './components/WeatherData/WeatherData';
import { WeatherMeasurementOfDay } from './models/Measurement';
import { weatherService } from './services/weatherService/weatherService';
import './App.css';
import { getReadableDateFromTimestamp } from './utils/getReadableDate';

function App() {
  const [weatherData, setWeatherData] = useState<WeatherMeasurementOfDay[]>([]);
  const [filteredData, setFilterData] = useState<WeatherMeasurementOfDay[]>([]);

  const getMeasurements = () => {
    const makeRequest = async () => {
      weatherService
        .getAllMeasurements()
        .then(async (data) => {
          if (data.status === 200) {
            const json = await data.json();
            setWeatherData(json);
            setFilterData(json);
          }
          if (data.status === 404) {
            alert(
              'Неверно указан адрес или порт подключения к серверу. Проверьте параметры конфигурации и попробуйте снова!'
            );
          }
        })
        .catch((error) => {
          alert(
            `Ошибка при загрузке данных! Проверьте, верно ли указан адрес и порт сервера, а также состояние самого сервера!\n${error}`
          );
        });
    };
    makeRequest();
  };

  const searchFn = (value: string, by: 'date' | 'morning') => {
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
