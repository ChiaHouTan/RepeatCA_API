using GamesAPI.DTOs;
using GamesAPI.Models;
using GamesAPI.Services;
using Mongo2Go;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GamesAPI.Tests
{
    public class DeveloperServiceTests : IDisposable
    {
        private readonly MongoDbRunner _runner;
        private readonly IMongoCollection<DeveloperItem> _developerCollection;
        private readonly IMongoCollection<GameItem> _gameCollection;
        private readonly DeveloperService _developerService;
        private readonly GameService _gameService;

        public DeveloperServiceTests()
        {
            // Start Mongo2Go in-memory MongoDB instance
            _runner = MongoDbRunner.Start();

            var client = new MongoClient(_runner.ConnectionString);
            var database = client.GetDatabase("TestDb");

            _developerCollection = database.GetCollection<DeveloperItem>("Developers");
            _gameCollection = database.GetCollection<GameItem>("Games");

            _gameService = new GameService(_gameCollection);
            _developerService = new DeveloperService(_developerCollection, _gameService);
        }

        public void Dispose()
        {
            _runner.Dispose();
        }

        [Fact]
        public async Task GetAllWithGamesAsync_ReturnsDeveloperWithGamesDto()
        {
            // Arrange
            var dev = new DeveloperItem
            {
                Id = ObjectId.GenerateNewId().ToString(),
                DeveloperName = "DevOne",
                FoundedYear = new DateTime(2020, 1, 1),
                Country = "USA",
                IsIndependent = true
            };
            await _developerCollection.InsertOneAsync(dev);

            var game = new GameItem
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Title = "Game One",
                DeveloperId = dev.Id,
                ReleaseDate = DateTime.Now,
                Genre = GameItem.GenreType.Action,
                Price = 10,
                Rating = 4
            };
            await _gameService.CreateAsync(game);

            // Act
            var result = await _developerService.GetAllWithGamesAsync();

            // Assert
            Assert.Single(result);
            var devResult = result.First();
            Assert.Equal("DevOne", devResult.DeveloperName);
            Assert.Single(devResult.Games);
            Assert.Equal("Game One", devResult.Games.First().Title);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllDevelopers()
        {
            // Arrange
            var dev = new DeveloperItem
            {
                Id = ObjectId.GenerateNewId().ToString(),
                DeveloperName = "DevOne"
            };
            await _developerCollection.InsertOneAsync(dev);

            // Act
            var result = await _developerService.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("DevOne", result.First().DeveloperName);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsDeveloper()
        {
            // Arrange
            var dev = new DeveloperItem
            {
                Id = ObjectId.GenerateNewId().ToString(),
                DeveloperName = "DevOne"
            };
            await _developerCollection.InsertOneAsync(dev);

            // Act
            var result = await _developerService.GetByIdAsync(dev.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("DevOne", result.DeveloperName);
        }

        [Fact]
        public async Task GetByNameAsync_ReturnsMatchingDevelopers()
        {
            // Arrange
            var dev1 = new DeveloperItem
            {
                Id = ObjectId.GenerateNewId().ToString(),
                DeveloperName = "DevOne"
            };
            var dev2 = new DeveloperItem
            {
                Id = ObjectId.GenerateNewId().ToString(),
                DeveloperName = "DevTwo"
            };
            await _developerCollection.InsertManyAsync(new[] { dev1, dev2 });

            // Act
            var result = await _developerService.GetByNameAsync("DevOne");

            // Assert
            Assert.Single(result);
            Assert.Equal("DevOne", result.First().DeveloperName);
        }

        [Fact]
        public async Task CreateAsync_InsertsDeveloper()
        {
            // Arrange
            var dev = new DeveloperItem
            {
                Id = ObjectId.GenerateNewId().ToString(),
                DeveloperName = "DevCreate"
            };

            // Act
            await _developerService.CreateAsync(dev);
            var allDevs = await _developerService.GetAllAsync();

            // Assert
            Assert.Single(allDevs);
            Assert.Equal("DevCreate", allDevs.First().DeveloperName);
        }

        [Fact]
        public async Task UpdateAsync_ReplacesDeveloper()
        {
            // Arrange
            var dev = new DeveloperItem
            {
                Id = ObjectId.GenerateNewId().ToString(),
                DeveloperName = "DevOld"
            };
            await _developerCollection.InsertOneAsync(dev);

            // Modify
            dev.DeveloperName = "DevUpdated";

            // Act
            await _developerService.UpdateAsync(dev.Id, dev);
            var updatedDev = await _developerService.GetByIdAsync(dev.Id);

            // Assert
            Assert.NotNull(updatedDev);
            Assert.Equal("DevUpdated", updatedDev.DeveloperName);
        }

        [Fact]
        public async Task DeleteAsync_RemovesDeveloper()
        {
            // Arrange
            var dev = new DeveloperItem
            {
                Id = ObjectId.GenerateNewId().ToString(),
                DeveloperName = "DevToDelete"
            };
            await _developerCollection.InsertOneAsync(dev);

            // Act
            await _developerService.DeleteAsync(dev.Id);
            var result = await _developerService.GetByIdAsync(dev.Id);

            // Assert
            Assert.Null(result);
        }
    }
}
