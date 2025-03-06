using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
namespace StorageService.Database
{
    public class MeasureContext : DbContext 
    { 
        public DbSet<DbMeasure> measures { get; set; }
        public DbSet<DbWindDirection> windDirections { get; set; }
        public DbSet<DbPrecipitationType> precipitationTypes { get; set; }
        public DbSet<DbDayPart> dayParts { get; set; }
        public MeasureContext(DbContextOptions<MeasureContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbDayPart>().HasData(
                new DbDayPart { Id = 1, Description = "Утро" },
                new DbDayPart { Id = 2, Description = "День" },
                new DbDayPart { Id = 3, Description = "Вечер" }
            );
            modelBuilder.Entity<DbWindDirection>().HasData(
                new DbWindDirection { Id = 1, Description = "Южный" },
                new DbWindDirection { Id = 2, Description = "Северный" },
                new DbWindDirection { Id = 3, Description = "Западный" },
                new DbWindDirection { Id = 4, Description = "Восточный" },
                new DbWindDirection { Id = 5, Description = "Юго-западный" },
                new DbWindDirection { Id = 6, Description = "Юго-восточный" },
                new DbWindDirection { Id = 7, Description = "Северо-западный" },
                new DbWindDirection { Id = 8, Description = "Северо-восточный" }
            );
            modelBuilder.Entity<DbPrecipitationType>().HasData(
                new DbPrecipitationType { Id = 1, Description = "Облачно" },
                new DbPrecipitationType { Id = 2, Description = "Солнечно" },
                new DbPrecipitationType { Id = 3, Description = "Дождь" },
                new DbPrecipitationType { Id = 4, Description = "Снег" },
                new DbPrecipitationType { Id = 5, Description = "Снег с дождем" },
                new DbPrecipitationType { Id = 6, Description = "Облачно с прояснениями" }
            );
        }
    }
}
