using System.ComponentModel.DataAnnotations;

namespace GamesAPI.Models
{
    public class DeveloperItem
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string? DeveloperName { get; set; }
        public DateTime FoundedYear { get; set; }
        public string? Country { get; set; }
        public bool IsIndependent { get; set; }
        public ICollection<GameItem> Games { get; set; } = new List<GameItem>();
    }
}
