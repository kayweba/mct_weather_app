import { ConfigForm } from "./ConfigForm/ConfigForm";
import { WeatherMeasurementForm } from "./WeatherMeasurementForm/WeatherMeasurementForm";
import cls from './LeftPanel.module.css'

type Props = {
  getMeasurements: () => void
}

export function LeftPanel({ getMeasurements }: Props) {
  return (
    <div className={cls.leftPanel}>
      <ConfigForm />
      <div className={cls.line}></div>
      <WeatherMeasurementForm getMeasurements={getMeasurements} />
    </div>
  )
}