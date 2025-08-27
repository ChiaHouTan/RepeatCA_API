using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GamesAPI.Models
{
    public class DeveloperItem
    {
        [BsonId] // MongoDB primary key
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("DeveloperName")]
        public string? DeveloperName { get; set; }

        [BsonElement("FoundedYear")]
        public DateTime FoundedYear { get; set; }

        [BsonElement("Country")]
        public string? Country { get; set; }

        [BsonElement("IsIndependent")]
        public bool IsIndependent { get; set; }
    }

    public class DeveloperDto
    {
        public string? Id { get; set; }
        public string? DeveloperName { get; set; }
        public int FoundedYear { get; set; } // int instead of DateTime
        public string? Country { get; set; }
        public bool IsIndependent { get; set; }
    }
}
