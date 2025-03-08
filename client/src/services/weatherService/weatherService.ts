import { WeatherMeasurement } from "../../models/Measurement";
import { BASE_URL } from "../../utils/getURLFromLocalStorage";

class WeatherService {
  public async getAllMeasurements() {
    const response = await fetch(`${BASE_URL}/measures`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json;'
      }
    })
    return response
  }

  public async sendMeasurement (data: WeatherMeasurement) {
    const response = await fetch(`${BASE_URL}/measures`, {
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
