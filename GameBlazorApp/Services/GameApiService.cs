using System.Net.Http.Json;
using GameBlazorApp.Models;

public class GameApiService : IGameApiService
{
    private readonly HttpClient _http;

    public GameApiService(IHttpClientFactory httpFactory)
    {
        _http = httpFactory.CreateClient("GamesAPI");
    }

    public async Task<List<GameDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<GameDto>>("api/games");
    }

    public async Task<List<GameDto>> GetByGenreAsync(string genre)
    {
        return await _http.GetFromJsonAsync<List<GameDto>>($"api/games/byGenre/{genre}");
    }

    public async Task<GameDto> GetByIdAsync(string id)
    {
        return await _http.GetFromJsonAsync<GameDto>($"api/games/{id}");
    }

    public async Task<GameDto2> CreateAsync(GameDto2 dto)
    {
        var response = await _http.PostAsJsonAsync("api/games", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<GameDto2>();
    }

    public async Task UpdateAsync(string id, GameDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/games/{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(string id)
    {
        var response = await _http.DeleteAsync($"api/games/{id}");
        response.EnsureSuccessStatusCode();
    }
}
