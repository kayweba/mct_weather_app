import { WeatherMeasurement } from "../../models/Measurement";

class WeatherService {
  public async getAllMeasurements() {
    const response = await fetch(`${import.meta.env.VITE_BASE_URL}/measures`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json;'
      }
    })
    return response
  }

  public async sendMeasurement (data: WeatherMeasurement) {
    const response = await fetch(`${import.meta.env.VITE_BASE_URL}/measures`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json;'
      },
      body: JSON.stringify(data)
    })
    
    return response
  }
}

export const weatherService = new WeatherService()
