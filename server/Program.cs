using StorageService;
using System.Reflection;

Version? ver = Assembly.GetExecutingAssembly().GetName().Version;
string version = "0.0.1";
if (ver is not null)
    version = ver.ToString();
Console.WriteLine($"Версия сервера: {version}");
Console.WriteLine($"Время запуска сервера {DateTime.Now}");

ConfigManager config = new ConfigManager();
