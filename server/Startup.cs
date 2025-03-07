using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StorageService.Database;

namespace StorageService.Web
{
    public class Startup
    {
        private Microsoft.Extensions.Configuration.IConfigurationRoot Configuration { get; }

        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = (IConfigurationRoot)configuration;
        }

        // Метод используется для добавления сервисов в контейнер
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            DBMS_Type dbms = DBMS_Type.UNDEFINED;
            Enum.TryParse(Configuration.GetSection("dbms").Get<string>(), out dbms);
            string? connection = Configuration.GetSection("connection_string").Get<string>();
            LogManager.Instance().Log($"{{Startup.cs}} dbms = {dbms}, connection = {connection}", MType.Information);
            services.AddDbContext<MeasureContext>(options =>
            {
                if (dbms == DBMS_Type.PGSQL)
                    options.UseNpgsql(connection);
                else if (dbms == DBMS_Type.SQLITE)
                    options.UseSqlite($"FileName={connection}.db");
                else if (dbms == DBMS_Type.UNDEFINED)
                {
                    LogManager.Instance().Log($"СУБД не инициализирована. Будет использован SQLite.", MType.Warning, MSeverity.Critical);
                    connection = "weather";
                    options.UseSqlite($"FileName={connection}.db");
                }
            });
            services.AddLogging(
            builder =>
            {
                builder.AddFilter("default", LogLevel.Error);
                builder.SetMinimumLevel(LogLevel.Error);
            });
        }

        // Метод используется для перенаправления HTTP запросов в контроллер
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseRouting();
            app.UseCors(
                policy =>
                {
                    policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                }
            );
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
