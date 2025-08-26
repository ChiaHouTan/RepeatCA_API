using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesAPI.Models
{
    public class GameItem
    {
        public enum genre
        {
            Action,
            Adventure,
            RPG,
            Puzzle,
            Strategy,
            Simulation,
            Sports,
            Horror,
            Shooter,
            Racing,
            Other
        }
        [Key]
        public Guid ID { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public DateTime ReleaseDate { get; set; }
        public genre Genre { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        [ForeignKey("AlbumID")]
        public DeveloperItem? Developer { get; set; }
        public Guid DeveloperID { get; set; }
    }
}
