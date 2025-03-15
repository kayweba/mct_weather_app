import { WeatherMeasurementForm } from "./WeatherMeasurementForm/WeatherMeasurementForm";
import cls from './LeftPanel.module.css'

type Props = {
  getMeasurements: () => void
}

export function LeftPanel({ getMeasurements }: Props) {
  return (
    <div className={cls.leftPanel}>
      <WeatherMeasurementForm getMeasurements={getMeasurements} />
    </div>
  )
}