using GameBlazorApp.Models;

public interface IDeveloperApiService
{
    Task<List<DeveloperWithGamesDto>> GetAllWithGamesAsync();
    Task<List<DeveloperDto>> GetAllAsync();
    Task<List<DeveloperDto>> GetByNameAsync(string name);
}
