using GamesAPI.Models;
using MongoDB.Driver;

namespace GamesAPI.Services
{
    public class DeveloperService
    {
        private readonly IMongoCollection<DeveloperItem> _developers;

        public DeveloperService(IMongoCollection<DeveloperItem> developers)
        {
            _developers = developers;
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
