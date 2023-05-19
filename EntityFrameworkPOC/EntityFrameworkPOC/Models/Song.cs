using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkPOC.Models
{
    public class Song
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public Genre? Genre { get; set; }
        public double? Rating { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Guid? ArtistId { get; set; }
        public Artist? Artist { get; set; }
    }
}
