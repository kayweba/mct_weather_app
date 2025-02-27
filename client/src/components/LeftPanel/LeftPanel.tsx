import { ConfigForm } from "./ConfigForm/ConfigForm";
import { WeatherMeasurementForm } from "./WeatherMeasurementForm/WeatherMeasurementForm";
import cls from './LeftPanel.module.css'

export function LeftPanel() {
  return (
    <div className={cls.leftPanel}>
      <ConfigForm />
      <div className={cls.line}></div>
      <WeatherMeasurementForm />
    </div>
  )
}