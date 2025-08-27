using GamesAPI.Models;

public interface IGameService
{
    Task<List<GameItem>> GetByDeveloperIdAsync(string developerId);
    Task<List<GameItem>> GetAllAsync();
    Task<GameItem?> GetByIdAsync(string id);
    Task<List<GameItem>> GetByGenreAsync(string genre);
    Task CreateAsync(GameItem game);
    Task UpdateAsync(string id, GameItem game);
    Task DeleteAsync(string id);
}
