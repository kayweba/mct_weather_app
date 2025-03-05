using Microsoft.EntityFrameworkCore;
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
    }
}
