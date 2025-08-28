using GameBlazorApp.Models;

public interface IGameApiService
{
    Task<List<GameDto>> GetAllAsync();
    Task<List<GameDto>> GetByGenreAsync(string genre);
    Task<GameDto> GetByIdAsync(string id);
    Task<GameDto> CreateAsync(GameDto dto);
    Task UpdateAsync(string id, GameDto dto);
    Task DeleteAsync(string id);
}
