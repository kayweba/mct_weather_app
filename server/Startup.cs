using Microsoft.EntityFrameworkCore;
using StorageService.Database;

namespace StorageService.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // Метод используется для добавления сервисов в контейнер
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            DBMS_Type dbms = DBMS_Type.UNDEFINED;
            Enum.TryParse<DBMS_Type>(Configuration["dbms"], out dbms);
            string? connection = Configuration["connection_string"];
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
            //services.AddLogging(
            //builder =>
            //{
            //    builder.AddFilter("default", LogLevel.None);
            //});
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
