import { useEffect, useState } from 'react';
import { Input } from '../../UI/Input/Input';
import cls from './ConfigForm.module.css';
import { Button } from '../../../components/UI/Button/Button';
import { ConfigKeys, LogLevel, LogPlace } from '../../../models/Config';

export function ConfigForm() {
  const [address, setAddress] = useState<string>('');
  const [port, setPort] = useState<string>('');
  const [logLevel, setLogLevel] = useState<LogLevel>(LogLevel.FATAL);
  const [logPlace, setLogPlace] = useState<LogPlace>(LogPlace.CONSOLE);

  useEffect(() => {
    const savedAddress = localStorage.getItem(ConfigKeys.ADDRESS);
    const savedPort = localStorage.getItem(ConfigKeys.PORT);
    const savedLogLevel = localStorage.getItem(ConfigKeys.LOG_LEVEL);
    const savedLogPlace = localStorage.getItem(ConfigKeys.LOG_PLACE);

    if (savedAddress) setAddress(savedAddress);
    if (savedPort) setPort(savedPort);
    if (savedLogLevel) setLogLevel(Number(savedLogLevel));
    if (savedLogPlace) setLogPlace(Number(savedLogPlace));
  }, []);

  const saveConfigInLocalStorage = () => {
    try {
      if (address) localStorage.setItem(ConfigKeys.ADDRESS, address);
      if (port) localStorage.setItem(ConfigKeys.PORT, port);
      if (logLevel) localStorage.setItem(ConfigKeys.LOG_LEVEL, String(logLevel));
      if (logPlace) localStorage.setItem(ConfigKeys.LOG_PLACE, String(logPlace));
      alert('Конфигурация успешно сохранена!')
    } catch (error) {
      alert(`Возникла ошибка при сохранении конфигурации!\n${error}`)
    }
  };

  return (
    <div>
      <p className={cls.title}>Настройки</p>
      <div className={cls.container}>
        <Input
          value={address}
          onChange={(event) => setAddress(event.currentTarget.value)}
          placeholder={'Адрес подключения к БД'}
        />
        <Input
          value={port}
          onChange={(event) => setPort(event.currentTarget.value)}
          placeholder={'Порт подключения к БД'}
        />
        <label htmlFor={'log_level'}>Уровень логирования</label>
        <select
          value={logLevel}
          onChange={(event) => setLogLevel(Number(event.currentTarget.value))}
          id={'log_level'}
          name={'log_level'}
        >
          <option value={LogLevel.FATAL}>FATAL</option>
          <option value={LogLevel.ERROR}>ERROR</option>
          <option value={LogLevel.WARNING}>WARN</option>
          <option value={LogLevel.INFO}>INFO</option>
          <option value={LogLevel.DEBUG}>DEBUG</option>
          <option value={LogLevel.TRACE}>TRACE</option>
        </select>
        <label htmlFor="log_place">Место для логирования</label>
        <select
          value={logPlace}
          onChange={(event) => setLogPlace(Number(event.currentTarget.value))}
          id="log_place"
        >
          <option value={LogPlace.CONSOLE}>Консоль</option>
          <option value={LogPlace.FILE}>Файл</option>
          <option value={LogPlace.NO_LOGS}>Отсутствие лога</option>
        </select>
        <Button onClick={saveConfigInLocalStorage}>
          Сохранить конфигурацию
        </Button>
      </div>
    </div>
  );
}
