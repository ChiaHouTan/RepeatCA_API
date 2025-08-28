namespace GameBlazorApp.Models;

public class DeveloperDto
{
    public string? Id { get; set; }
    public string? DeveloperName { get; set; }
    public int FoundedYear { get; set; }
    public string? Country { get; set; }
    public bool IsIndependent { get; set; }
}

public class GameDto
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Image { get; set; }
    public string? ReleaseDate { get; set; }   // keep as DateTime for form binding
    public string? Genre { get; set; }
    public double Price { get; set; }
    public double Rating { get; set; }
    public string? DeveloperId { get; set; }
}

public class DeveloperWithGamesDto
{
    public string? Id { get; set; }
    public string? DeveloperName { get; set; }
    public string? FoundedYear { get; set; }
    public string? Country { get; set; }
    public bool IsIndependent { get; set; }
    public List<GameDto> Games { get; set; } = new();
}
