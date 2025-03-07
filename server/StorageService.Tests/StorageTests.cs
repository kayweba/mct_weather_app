using Microsoft.EntityFrameworkCore;
using StorageService.Database;
namespace StorageService.Tests
{
    public class StrorageTests
    {
        private static string connectionStr = "FileName=werather.db";
        [Fact]
        public void GetPredefinedTablesNotNull()
        {
            DbContextOptionsBuilder<MeasureContext> builder = new DbContextOptionsBuilder<MeasureContext>();
            builder.UseSqlite(connectionStr);
            using (MeasureContext db = new MeasureContext(builder.Options))
            {
                List<DbWindDirection> winds = db.windDirections.ToList();
                List<DbPrecipitationType> precipitations = db.precipitationTypes.ToList();
                List<DbDayPart> dayParts = db.dayParts.ToList();
                Assert.NotEmpty(winds);
                Assert.NotEmpty(precipitations);
                Assert.NotEmpty(dayParts);
            }
        }
        [Fact]
        public void AddMeasureCorrect()
        {
            DbContextOptionsBuilder<MeasureContext> builder = new DbContextOptionsBuilder<MeasureContext>();
            builder.UseSqlite(connectionStr);
            using (MeasureContext db = new MeasureContext(builder.Options))
            {
                DbDayPart? dbDayPart = db.dayParts.Find(1);
                DbWindDirection? dbWind = db.windDirections.Find(1);
                DbPrecipitationType? dbPrecipitation = db.precipitationTypes.Find(1);

                Assert.NotNull(dbDayPart);
                Assert.NotNull(dbWind);
                Assert.NotNull(dbPrecipitation);
                DbMeasure measure = new DbMeasure
                {
                    Measure_date = 1002,
                    Day_part = dbDayPart!,
                    Measure_day_part = dbDayPart!.Id,
                    Precipitation_typeId = dbPrecipitation!.Id,
                    Precipitation = dbPrecipitation!,
                    Pressure = 2,
                    Temperature = 3,
                    Wind = dbWind!,
                    Wind_directionId = dbWind!.Id,
                    Wind_speed = 4
                };
                db.measures.Add(measure);
                Assert.NotEqual(0, db.SaveChanges());
            }
        }
        [Fact]
        public void AddMeasureWrong()
        {
            DbContextOptionsBuilder<MeasureContext> builder = new DbContextOptionsBuilder<MeasureContext>();
            builder.UseSqlite(connectionStr);
            using (MeasureContext db = new MeasureContext(builder.Options))
            {
                DbDayPart? dbDayPart = db.dayParts.Find(1);
                Assert.NotNull(dbDayPart);
                DbMeasure measure = new DbMeasure
                {
                    Measure_date = 1001,
                    Day_part = dbDayPart!,
                    Measure_day_part = 1,
                    Precipitation_typeId = 0,
                    Precipitation = null,
                    Pressure = 2,
                    Temperature = 3,
                    Wind = null,
                    Wind_directionId = 0,
                    Wind_speed = 4
                };
                db.measures.Add(measure);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    Assert.Contains("'FOREIGN KEY constraint failed'", 
                        ex.InnerException!.Message);
                }
                try
                {
                    measure.Precipitation_typeId = 1;
                    db.measures.Add(measure);
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    Assert.Contains("'FOREIGN KEY constraint failed'",
                        ex.InnerException!.Message);
                }
                measure.Wind_directionId = 1;
                db.measures.Add(measure);
                Assert.NotEqual(0, db.SaveChanges());
            }
        }
    }
}