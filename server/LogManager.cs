namespace StorageService
{
    internal class LogManager 
    {
        public static LogConfiguration? Configuration
        {
            get => configuration;
            set
            {
                configuration = value;
                if (instance is not null && configuration is not null)
                    instance.UpdateConfiguration(configuration);
            }
        }
        public static LogManager Instance()
        {
            if (instance is null)
            {
                bool emptyConfiguration = false;
                if (configuration is null)
                {
                    // Конфигурация не задана. Создаем конфигурацию по умолчанию
                    configuration = new LogConfiguration();
                    emptyConfiguration = true;
                }
                instance = new LogManager(configuration);
                if (emptyConfiguration)
                    instance.Log("Конфигурация логера была создана автоматически.", MType.Warning);
            }
            return instance;
        }
        private static LogManager? instance;
        private static LogConfiguration? configuration;
        private LogManager(LogConfiguration _configuration)
        {
            configuration = _configuration;
            loggers.Clear();
            if (configuration.Destination.Contains(LOG_Destination.CONSOLE))
                InitializeConsoleLog();
            if (configuration.Destination.Contains(LOG_Destination.FILE))
                InitializeFileLog(configuration.Path);
        }
        private void InitializeConsoleLog()
        {
            loggers.Add(new ConsoleLogger());
        }
        private void InitializeFileLog(string? path)
        {
            FileLogger fl = new FileLogger();
            if (!string.IsNullOrEmpty(path))
                fl.Path = path;
            loggers.Add(fl);
        }
        private void UpdateConfiguration(LogConfiguration configuration)
        {
            string destination = "";
            foreach (LOG_Destination dest in configuration.Destination)
            {
                switch (dest)
                {
                    case LOG_Destination.CONSOLE:
                        destination += "console";
                        break;
                    case LOG_Destination.FILE:
                        destination += "file";
                        break;
                    case LOG_Destination.SILENCE:
                        destination += "silence";
                        break;
                }
                destination += ";";
            }
            destination = destination.Remove(destination.Length - 1);
            Log($"Конфигурация логера была обновлена. Новая конфигурация: level - {configuration.Level}, " +
                $"destination - [{destination}]", MType.Information, MSeverity.Important);
            loggers.Clear();
            if (configuration.Destination.Contains(LOG_Destination.CONSOLE))
                InitializeConsoleLog();
            if (configuration.Destination.Contains(LOG_Destination.FILE))
                InitializeFileLog(configuration.Path);
        }
        public void Log(string message, MType type, MSeverity severity = MSeverity.Other)
        {
            if (configuration is not null && (uint)severity <= configuration.Level)
            {
                logAccess.WaitOne();
                // Подготавливаем сообщение
                message = '[' + DateTime.Now.TimeOfDay.ToString() + "] " + message;
                try
                {
                    foreach (ILogger logger in loggers)
                    {
                        switch (type)
                        {
                            case MType.Error:
                                logger.TraceError(message);
                                break;
                            case MType.Warning:
                                logger.TraceWarning(message);
                                break;
                            case MType.Information:
                                logger.TraceInfo(message);
                                break;
                            case MType.Message:
                                logger.Trace(message);
                                break;
                            default:
                                logger.Trace(message);
                                break;
                        }
                    }
                }
                catch (LogException ex)
                {
                    // Ошибки логируем не больше одного раза, чтобы не спамить
                    if (!errorCounters.Contains((LogErrorCode) ex.ErrorCode))
                    {
                        // Не удалось записать лог в файл или консоль
                        // выведем сообщение об ошибке в консоль
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        errorCounters.Add((LogErrorCode) ex.ErrorCode);
                    }
                }
                logAccess.ReleaseMutex();
            }
        }
        private List<ILogger> loggers = new List<ILogger>();
        private Mutex logAccess = new Mutex();
        private List<LogErrorCode> errorCounters = new List<LogErrorCode>();
    }
    public enum MType
    {
        Error,
        Warning,
        Information,
        Message
    }
    public enum MSeverity
    {
        Critical,
        Important,
        Info,
        Other
    }
}
