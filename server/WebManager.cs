using Microsoft.AspNetCore.Hosting.Server.Features;
using StorageService.Database;

namespace StorageService.Web
{
    public class WebManager
    {
        public WebManager(ConnConfiguration? configuration, DbConfiguration? dbConfig)
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
            string dbConnectionString = "weather";
            DBMS_Type dbms = DBMS_Type.UNDEFINED;
            if (dbConfig is not null)
            {
                dbms = dbConfig.DBMS;
                if (dbConfig.DBMS == DBMS_Type.PGSQL)
                    dbConnectionString = $"Host={dbConfig.Host.ToString()};" +
                        $"Port={dbConfig.Port};" +
                        $"Database=weather;" +
                        $"Username={dbConfig.User};" +
                        $"Password={dbConfig.Password}";
            }

            LogManager.Instance().Log($"dbms = {dbms}, connection_string = {dbConnectionString}", MType.Information, MSeverity.Other);
            
            host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseSetting("dbms", dbms.ToString())
                .UseSetting("connection_string", dbConnectionString)
                .UseUrls($"http://{hostAddress}:{hostPort}/")
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
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
