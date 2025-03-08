import { useEffect, useState } from 'react';

import { LeftPanel } from './components/LeftPanel/LeftPanel';
import { WeatherData } from './components/WeatherData/WeatherData';
import { WeatherMeasurementOfDay } from './models/Measurement';
import { weatherService } from './services/weatherService/weatherService';
import './App.css';

function App() {
  const [weatherData, setWeatherData] = useState<WeatherMeasurementOfDay[]>([])

  const getMeasurements = () => {
    const makeRequest = async () => {
        weatherService.getAllMeasurements().then(async (data) => {
          if (data.status === 200) {
            setWeatherData(await data.json())
          }
          if (data.status === 404) {
            alert('Неверно указан адрес или порт подключения к серверу. Проверьте параметры конфигурации и попробуйте снова!')
          }
        }).catch((error) => {
          alert(`Ошибка при загрузке данных! Проверьте, верно ли указан адрес и порт сервера, а также состояние самого сервера!\n${error}`)
        })
    }
    makeRequest()
  }

  useEffect(() => {
    getMeasurements()
  }, [])


  return (
    <div className={'container'}>
      <LeftPanel getMeasurements={getMeasurements}/>
      <WeatherData data={weatherData} />
    </div>
  );
}

export default App;
