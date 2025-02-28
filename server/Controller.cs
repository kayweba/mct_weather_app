using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService
{
    internal class Controller
    {
        public Controller() {
            configuration = new ConfigManager();
            if (configuration.Logger is not null)
                LogManager.Configuration = configuration.Logger;
                
        }
        public void Start()
        {
            LogManager.Instance().Log("Запуск controller.", MType.Information);
        }
        public void Stop()
        {
            LogManager.Instance().Log("Остановка controller.", MType.Information);
        }

        private IConfiguration configuration;
    }
}
