using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageService.Database;
using StorageService.Web;
using StorageService.Web.Controllers;
using StorageService.Web.Models;

namespace StorageService.Tests
{
    public class WebTests
    {
        private static string connectionStr = "FileName=werather.db";
        [Fact]
        public void GetRequestCorrect()
        {
            DbContextOptionsBuilder<MeasureContext> builder = new DbContextOptionsBuilder<MeasureContext>();
            builder.UseSqlite(connectionStr);
            using (MeasureContext db = new MeasureContext(builder.Options))
            {
                MeasuresController controller = new MeasuresController(db);
                ActionResult<List<Measure>> measuresResult = controller.Get();
                Assert.NotNull(measuresResult.Value);
            }
        }
        [Fact]
        public void PostRequestCorrect()
        {
            DbContextOptionsBuilder<MeasureContext> builder = new DbContextOptionsBuilder<MeasureContext>();
            builder.UseSqlite(connectionStr);
            using (MeasureContext db = new MeasureContext(builder.Options))
            {
                MeasuresController controller = new MeasuresController(db);
                PMeasure postedMeasure = new PMeasure
                {
                    date = ((DateTimeOffset)DateTime.Today).ToUnixTimeSeconds(),
                    part_of_day = 1,
                    temperature = 10,
                    wind_speed = 5,
                    pressure = 755,
                    wind_direction = 1,
                    precipitation_type = 1,
                    force_overwrite = false
                };
                ActionResult<PMeasure?> ret = controller.Post(postedMeasure);
                Assert.NotNull(ret.Value);
            }
        }
    }
}
