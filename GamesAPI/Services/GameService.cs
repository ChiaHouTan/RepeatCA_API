using GamesAPI.Models;
using MongoDB.Driver;

namespace GamesAPI.Services
{
    public class GameService
    {
        private readonly IMongoCollection<GameItem> _games;

        public GameService(IMongoCollection<GameItem> games)
        {
            _games = games;
        }

        public async Task<List<GameItem>> GetAllAsync() =>
            await _games.Find(_ => true).ToListAsync();

        public async Task<List<GameItem>> GetByDeveloperIdAsync(string developerId) =>
            await _games.Find(g => g.DeveloperId == developerId).ToListAsync();

        public async Task<GameItem?> GetByIdAsync(string id) =>
            await _games.Find(g => g.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(GameItem game) =>
            await _games.InsertOneAsync(game);

        public async Task UpdateAsync(string id, GameItem game) =>
            await _games.ReplaceOneAsync(g => g.Id == id, game);

        public async Task DeleteAsync(string id) =>
            await _games.DeleteOneAsync(g => g.Id == id);
    }
}
