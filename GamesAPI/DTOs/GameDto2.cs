using System;

namespace GamesAPI.DTOs
{
    public class GameDto2
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public DateTime ReleaseDate { get; set; } // formatted as dd.MM.yyyy
        public string? Genre { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public string? DeveloperId { get; set; }
    }
}
