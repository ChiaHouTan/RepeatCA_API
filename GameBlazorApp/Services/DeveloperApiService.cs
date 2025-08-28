using System.Net.Http.Json;
using GameBlazorApp.Models;

public class DeveloperApiService : IDeveloperApiService
{
    private readonly HttpClient _http;

    public DeveloperApiService(IHttpClientFactory httpFactory)
    {
        _http = httpFactory.CreateClient("GamesAPI");
    }

    public async Task<List<DeveloperWithGamesDto>> GetAllWithGamesAsync()
    {
        return await _http.GetFromJsonAsync<List<DeveloperWithGamesDto>>("api/developers/withGames");
    }

    public async Task<List<DeveloperDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<DeveloperDto>>("api/developers");
    }

    public async Task<List<DeveloperDto>> GetByNameAsync(string name)
    {
        return await _http.GetFromJsonAsync<List<DeveloperDto>>($"api/Developers/byName?name={name}");
    }
}
