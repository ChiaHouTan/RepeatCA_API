using GamesAPI.DTOs;
using GamesAPI.Models;
using MongoDB.Driver;

namespace GamesAPI.Services
{
    public class DeveloperService
    {
        private readonly IMongoCollection<DeveloperItem> _developers;
        private readonly GameService _gameService;

        public DeveloperService(IMongoCollection<DeveloperItem> developers, GameService gameService)
        {
            _developers = developers;
            _gameService = gameService;
        }

        public async Task<List<DeveloperWithGamesDto>> GetAllWithGamesAsync()
        {
            var developers = await _developers.Find(_ => true).ToListAsync();
            var result = new List<DeveloperWithGamesDto>();

            foreach (var dev in developers)
            {
                // Use GameService to get games by developer
                var games = await _gameService.GetByDeveloperIdAsync(dev.Id);

                result.Add(new DeveloperWithGamesDto
                {
                    Id = dev.Id,
                    DeveloperName = dev.DeveloperName,
                    FoundedYear = dev.FoundedYear,
                    Country = dev.Country,
                    IsIndependent = dev.IsIndependent,
                    Games = games.Select(g => new GameDto2
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Image = g.Image,
                        ReleaseDate = g.ReleaseDate,
                        Genre = g.Genre.ToString(),
                        Price = g.Price,
                        Rating = g.Rating,
                        DeveloperId = g.DeveloperId
                    }).ToList()
                });
            }

            return result;
        }

        public async Task<List<DeveloperItem>> GetAllAsync() =>
            await _developers.Find(_ => true).ToListAsync();

        public async Task<DeveloperItem?> GetByIdAsync(string id) =>
            await _developers.Find(d => d.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(DeveloperItem dev) =>
            await _developers.InsertOneAsync(dev);

        public async Task UpdateAsync(string id, DeveloperItem dev) =>
            await _developers.ReplaceOneAsync(d => d.Id == id, dev);

        public async Task DeleteAsync(string id) =>
            await _developers.DeleteOneAsync(d => d.Id == id);
    }
}
