namespace StorageService.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // Метод используется для добавления сервисов в контейнер
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddLogging(
            builder =>
            {
                builder.AddFilter("default", LogLevel.None);
            });
        }

        // Метод используется для перенаправления HTTP запросов в контроллер
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
