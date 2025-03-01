import './App.css';
import { LeftPanel } from './components/LeftPanel/LeftPanel';
import { WeatherData } from './components/WeatherData/WeatherData';

function App() {
  return (
    <div className={'container'}>
      <LeftPanel />
      <WeatherData />
    </div>
  );
}

export default App;
