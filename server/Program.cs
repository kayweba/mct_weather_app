using StorageService;
using System.Reflection;

Version? ver = Assembly.GetExecutingAssembly().GetName().Version;
string version = "0.0.1";
if (ver is not null)
    version = ver.ToString();
LogManager.Instance().Log($"Версия сервера: {version}", MType.Information);
LogManager.Instance().Log($"Время запуска сервера {DateTime.Now}", MType.Information);

Controller server = new Controller(); // Создаем экземпляр контроллера
server.Start(); // Запускаем сервер

Console.WriteLine("Нажмите Esc для завершения...");
do
{

} while (Console.ReadKey(true).Key != ConsoleKey.Escape);

server.Stop(); // Останавливаем сервер
LogManager.Instance().Log("Приложение завершает свою работу", MType.Information);
