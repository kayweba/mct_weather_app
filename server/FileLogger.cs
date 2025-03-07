using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace StorageService
{
    class FileLogger : ILogger
    {
        public FileLogger()
        {
            fileName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + "_" +
            DateTime.UtcNow.Date.ToString("dd/MM/yyyy") + "_log.txt";
            filePath = fileName;
        }
        public void Trace(string message)
        {
            TraceInternal(message);
        }

        public void TraceError(string errorMessage)
        {
            TraceInternal($"[E] {errorMessage}");
        }

        public void TraceInfo(string info)
        {
            TraceInternal($"[I] {info}");
        }

        public void TraceWarning(string warningMessage)
        {
            TraceInternal($"[W] {warningMessage}");
        }
        public string? Path
        {
            get => path; 
            set {
                path = value;
                if (path is not null)
                {
                    // Проверим, что нет слеша на конце и заменим все обратные слеши прямыми
                    path = path.Replace('\\', '/');
                    if (path.EndsWith("\\")) path = path.Remove(path.Length - 1);
                    filePath = path + '/' + fileName;
                }
            }
        }
        private void TraceInternal(string message)
        {
            try
            {
                using (StreamWriter logFile = new StreamWriter(filePath, true))
                {
                    logFile.WriteLine(message);
                }
            }
            catch (DirectoryNotFoundException)
            {
                throw new LogException($"Не удалось найти директорию \"{path}\" для записи лог-файла.", (int) LogErrorCode.dirNotFound);
            }
            catch (IOException ex)
            {
                throw new LogException($"Не удалось записать лог в файл. {ex.Message}.", (int) LogErrorCode.ioFailed);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new LogException($"Не удалось записать лог в файл. Ошибка доступа. {ex.Message}.", (int) LogErrorCode.ioForbidden);
            }
        }
        private string? path;
        private string fileName;
        private string filePath;
    }
}
