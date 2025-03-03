using System.ComponentModel.DataAnnotations;

namespace StorageService.Database
{
    public class DbMeasure
    {
        [Key]
        public long Measure_date { get; set; }
        public double? Morning_temperature { get; set; }
        public double? Morning_pressure { get; set; }
        public double? Morning_wind_speed { get; set; }
        public DbWindDirection? Morning_wind_direction { get; set; }
        public DbPrecipitationType? Morning_precipitation_type { get; set; }
        public double? Afternoon_temperature { get; set; }
        public double? Afternoon_pressure { get; set; }
        public double? Afternoon_wind_speed { get; set; }
        public DbWindDirection? Afternoon_wind_direction { get; set; }
        public DbPrecipitationType? Afternoon_precipitation_type { get; set; }
        public double? Evening_temperature { get; set; }
        public double? Evening_pressure { get; set; }
        public double? Evening_wind_speed { get; set; }
        public DbWindDirection? Evening_wind_direction { get; set; }
        public DbPrecipitationType? Evening_precipitation_type { get; set; }
    }
}