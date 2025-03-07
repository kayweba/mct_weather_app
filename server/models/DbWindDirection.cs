using System.ComponentModel.DataAnnotations;
namespace StorageService.Database
{
    public class DbWindDirection
    {
        [Key]
        public required int Id { get; set; }
        public required string Description { get; set; }
    }
}
