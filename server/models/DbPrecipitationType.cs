using System.ComponentModel.DataAnnotations;
namespace StorageService.Database
{
    public class DbPrecipitationType
    {
        [Key]
        public int Id { get; set; }
        public required string Description { get; set; }
    }
}
