import { Input } from '../../UI/Input/Input';
import cls from './ConfigForm.module.css';

export function ConfigForm() {
  return (
    <div>
      <p className={cls.title}>Настройки</p>
      <div className={cls.container}>
        <Input placeholder={'Адрес подключения к БД'} />
        <Input placeholder={'Порт подключения к БД'} />
        <label htmlFor={'log_level'}>Уровень логирования</label>
        <select defaultValue={'0'} id={'log_level'} name={'log_level'}>
          <option value={'0'}>FATAL</option>
          <option value={'1'}>ERROR</option>
          <option value={'2'}>WARN</option>
          <option value={'3'}>INFO</option>
          <option value={'4'}>DEBUG</option>
          <option value={'5'}>TRACE</option>
        </select>
        <label htmlFor="log_place">Место для логирования</label>
        <select defaultValue={'0'} id="log_place">
          <option value={'0'}>Консоль</option>
          <option value={'1'}>Файл</option>
          <option value={'2'}>Отсутствие лога</option>
        </select>
      </div>
    </div>
  );
}
