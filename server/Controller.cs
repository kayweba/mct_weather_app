using StorageService.Web;

namespace StorageService
{
    internal class Controller
    {
        public Controller() {
            configuration = new ConfigManager();
            if (configuration.Logger is not null)
                LogManager.Configuration = configuration.Logger;
            webServer = new WebManager(configuration.Connection);
        }
        public void Start()
        {
            LogManager.Instance().Log("Запуск controller", MType.Information);
            webServer.Start();
        }
        public void Stop()
        {
            LogManager.Instance().Log("Остановка controller", MType.Information);
            webServer.Stop();
        }

        private IConfiguration configuration;
        private WebManager webServer;
    }
}
