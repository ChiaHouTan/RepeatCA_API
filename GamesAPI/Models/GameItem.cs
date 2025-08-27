using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GamesAPI.Models
{
    public class GameItem
    {
        public enum GenreType
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

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Title")]
        public string? Title { get; set; }

        [BsonElement("Image")]
        public string? Image { get; set; }

        [BsonElement("ReleaseDate")]
        public DateTime ReleaseDate { get; set; }

        [BsonElement("Genre")]
        public GenreType Genre { get; set; }

        [BsonElement("Price")]
        public double Price { get; set; }

        [BsonElement("Rating")]
        public double Rating { get; set; }


        // Reference to Developer
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("DeveloperId")]
        public string DeveloperId { get; set; } = null!;
    }

    public class GameDto
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public string? DeveloperId { get; set; }
    }
}
