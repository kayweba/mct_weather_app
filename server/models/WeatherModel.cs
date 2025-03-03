namespace StorageService.Web.Models
{
    public class Measure
    {
        public ulong date { get; set; }
        public double? morning_temperature { get; set; }
        public double? morning_pressure { get; set; }
        public double? morning_wind_speed { get; set; }
        public uint? morning_wind_direction { get; set; }
        public uint? morning_precipitation_type { get; set; }
        public double? afternoon_temperature { get; set; }
        public double? afternoon_pressure { get; set; }
        public double? afternoon_wind_speed { get; set; }
        public uint? afternoon_wind_direction { get; set; }
        public uint? afternoon_precipitation_type { get; set; }
        public double? evening_temperature { get; set; }
        public double? evening_pressure { get; set; }
        public double? evening_wind_speed { get; set; }
        public uint? evening_wind_direction { get; set; }
        public uint? evening_precipitation_type { get; set; }
    }
}
