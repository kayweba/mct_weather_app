using System.ComponentModel.DataAnnotations;
namespace StorageService.Database
{
    public class DbWindDirection
    {
        [Key]
        public int Id { get; set; }
        public required string Description { get; set; }
    }
}
