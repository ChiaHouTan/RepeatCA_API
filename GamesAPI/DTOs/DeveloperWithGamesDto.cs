using GamesAPI.Models;
using System;
using System.Collections.Generic;

namespace GamesAPI.DTOs
{
    public class DeveloperWithGamesDto
    {
        public string? Id { get; set; }
        public string? DeveloperName { get; set; }
        public DateTime FoundedYear { get; set; }
        public string? Country { get; set; }
        public bool IsIndependent { get; set; }
        public List<GameDto2> Games { get; set; } = new();
    }
}
