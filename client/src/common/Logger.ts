
enum LogLevel {
  ERROR='error',
  WARN='warn',
  DEBUG='debug',
  VERBOSE='verbose',
}
type LogParams = {
  level: LogLevel
}

export type LogFn = {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  (message?: any, ...optionalParams: any[]): void
}

// eslint-disable-next-line @typescript-eslint/no-unused-vars, @typescript-eslint/no-explicit-any
const NO_LOG: LogFn = (_?: any, ...__: any[]) => {}

class Logger {
  private static sInstance: Logger
  public static get getInstance() {
      return this.sInstance || (this.sInstance = new this())
  }

  public readonly D: LogFn

  public readonly E: LogFn

  public readonly V: LogFn

  public readonly W: LogFn

  private constructor() {
      const param: LogParams = { level: LogLevel.DEBUG }
      const envLL = import.meta.env.VITE_LOG_LEVEL

      if (Object.values(LogLevel).includes(envLL)) {
          param.level = envLL
      } else {
          param.level = LogLevel.WARN
      }

      this.E = console.error
      if (param.level === 'error') {
          this.D = NO_LOG
          this.V = NO_LOG
          this.W = NO_LOG
          return
      }

      this.W = console.warn
      if (param.level === 'warn') {
          this.D = NO_LOG
          this.V = NO_LOG
          return
      }

      this.D = console.debug
      if (param.level === 'debug') {
          this.V = NO_LOG
          return
      }

      this.V = console.info
  }
}

export const LOG = Logger.getInstance