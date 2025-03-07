namespace StorageService.Web
{
    public class PMeasure
    {
        public static bool Validate(PMeasure value)
        {
            long unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            return ((value.date > 0 && value.date <= unixTime) &&
                    (value.part_of_day > 0 && value.part_of_day <= 3));
        }
        public long date { get; set; }
        public int part_of_day { get; set; }
        public int? precipitation_type { get; set; }
        public int? temperature { get; set; }
        public uint? pressure { get; set; }
        public uint? wind_speed { get; set; }
        public int? wind_direction { get; set; }
        public bool? force_overwrite { get; set; }
    }
}
