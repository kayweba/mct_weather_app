import { ConfigKeys } from "../models/Config"

const getURLFromLocalStorage = (): string | null => {
  const address = localStorage.getItem(ConfigKeys.ADDRESS)
  const port = localStorage.getItem(ConfigKeys.PORT)

  if (address && port) {
    return `${address}:${port}`
  }
  return null
}

export const BASE_URL = getURLFromLocalStorage()