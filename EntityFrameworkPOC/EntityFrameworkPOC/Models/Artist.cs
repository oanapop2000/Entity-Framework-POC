using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkPOC.Models
{
    public class Artist
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public List<Song>? Songs { get; set; }
    }
}
