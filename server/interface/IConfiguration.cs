namespace StorageService
{
    public interface IConfiguration
    {
        public DbConfiguration Database { get; }
        public ConnConfiguration Connection { get; }
        public LogConfiguration Logger { get; }
    }
}
