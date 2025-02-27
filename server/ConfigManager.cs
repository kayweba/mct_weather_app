namespace StorageService
{
    internal class ConfigManager : IConfiguration
    {
        public ConfigManager()
        {
            ReadConfiguration();
            //TODO: read the configuration file and parse it
            dbConfig = new DbConfiguration("", "", "", "");
            logConfig = new LogConfiguration(0, "");
            conConfig = new ConnConfiguration("", -1);
        }

        private void ReadConfiguration()
        {
            string configFileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + ".xml";

        }

        public DbConfiguration Database 
        {
            get => dbConfig;
        }

        public ConnConfiguration Connection
        {
            get => conConfig;
        }

        public LogConfiguration Logger
        {
            get => logConfig;
        }

        private DbConfiguration dbConfig;
        private LogConfiguration logConfig;
        private ConnConfiguration conConfig;
    }
}
