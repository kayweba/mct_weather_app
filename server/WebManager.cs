using Microsoft.AspNetCore.Hosting.Server.Features;

namespace StorageService.Web
{
    public class WebManager
    {
        public WebManager(ConnConfiguration? configuration)
        {
            // Если конфигурация не корректная, то запускаемся по умолчанию
            // на 127.0.0.1:80
            string hostAddress = "127.0.0.1";
            int hostPort = 80;
            if (configuration is not null)
            {
                hostAddress = configuration.Host.ToString();
                hostPort = configuration.Port;
            }
            host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseUrls($"http://{hostAddress}:{hostPort}/")
                .Build();
        }
        public void Start()
        {
            IServerAddressesFeature? addresses = host.ServerFeatures.Get<IServerAddressesFeature>();
            if (addresses is not null)
            {
                foreach (string address in addresses.Addresses)
                {
                    LogManager.Instance().Log($"Сервер ожидает подключения по адресу: {address}", MType.Information, MSeverity.Important);
                }
            }
            host.StartAsync();
            LogManager.Instance().Log($"WEB сервер запущен", MType.Information, MSeverity.Important);
        }
        public void Stop()
        {
            host.StopAsync();
            LogManager.Instance().Log("WEB сервер остановлен", MType.Information, MSeverity.Important);
        }
        private IWebHost host;
    }
}
