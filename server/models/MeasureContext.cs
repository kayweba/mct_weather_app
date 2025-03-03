using Microsoft.EntityFrameworkCore;
namespace StorageService.Database
{
    public class MeasureContext : DbContext 
    { 
        public DbSet<DbMeasure> measures { get; set; }
        public DbSet<DbWindDirection> windDirections { get; set; }
        public DbSet<DbPrecipitationType> precipitationTypes { get; set; }
        public MeasureContext(DbContextOptions<MeasureContext> options, DbConfiguration? _configuration) : base(options)
        {
            dbFileName = "weatherdb";
            configuration = _configuration;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (configuration is not null)
            {
                switch (configuration.DBMS)
                {
                    case DBMS_Type.SQLITE:
                        optionsBuilder.UseSqlite($"FileName={dbFileName}");
                        break;
                    case DBMS_Type.PGSQL:
                        string dbUser = "admin", dbPassword = "root", dbHost = "localhost";
                        int dbPort = 5433;
                        if (configuration is not null)
                        {
                            dbUser = configuration.User;
                            dbPassword = configuration.Password;
                            dbHost = configuration.Host.ToString();
                            dbPort = configuration.Port;
                        }
                        string conn = $"Host={dbHost};Port={dbPort};Database={dbFileName};Username={dbUser};Password={dbPassword}";
                        optionsBuilder.UseNpgsql(conn);
                        break;
                    default:
                        optionsBuilder.UseSqlite($"FileName={dbFileName}");
                        break;
                }
            }
            else
                optionsBuilder.UseSqlite($"FileName={dbFileName}");

        }
        private DbConfiguration? configuration;
        private string dbFileName;
    }
}
