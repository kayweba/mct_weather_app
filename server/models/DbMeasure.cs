using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageService.Database
{
    [PrimaryKey(nameof(Measure_date), nameof(Measure_day_part))]
    public class DbMeasure
    {
        public ulong Measure_date { get; set; }
        public int Measure_day_part { get; set; }
        [ForeignKey("Measure_day_part")]
        public required DbDayPart Day_part { get; set; }
        public double? Temperature { get; set; }
        public double? Pressure { get; set; }
        public double? Wind_speed { get; set; }
        public int Wind_directionId { get; set; }
        [ForeignKey("Wind_directionId")]
        public DbWindDirection? Wind { get; set; }
        public int Precipitation_typeId { get; set; }
        [ForeignKey("Precipitation_typeId")]
        public DbPrecipitationType? Precipitation { get; set; }
    }
}