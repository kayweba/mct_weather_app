export enum ConfigKeys {
  ADDRESS = 'address',
  PORT = 'port',
  LOG_LEVEL = 'log_level',
  LOG_PLACE = 'log_place'
}

export enum LogLevel {
  FATAL = 0,
  ERROR = 1,
  WARNING = 2,
  INFO = 3,
  DEBUG = 4,
  TRACE = 5,
}

export enum LogPlace {
  CONSOLE = 0,
  FILE = 1,
  NO_LOGS = 2,
}
