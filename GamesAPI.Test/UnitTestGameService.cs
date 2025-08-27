using GamesAPI.Models;
using GamesAPI.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Mongo2Go;

namespace GamesAPI.Tests
{
    public class UnitTestGameService
    {
        private readonly Mock<IMongoCollection<GameItem>> _mockCollection;
        private readonly GameService _service;

        public UnitTestGameService()
        {
            _mockCollection = new Mock<IMongoCollection<GameItem>>();
            _service = new GameService(_mockCollection.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllGames()
        {
            // Arrange
            var games = new List<GameItem>
            {
                new GameItem { Id = ObjectId.GenerateNewId().ToString(), Title = "Game 1" },
                new GameItem { Id = ObjectId.GenerateNewId().ToString(), Title = "Game 2" }
            };

            var mockCursor = new Mock<IAsyncCursor<GameItem>>();
            mockCursor.Setup(_ => _.Current).Returns(games);
            mockCursor.SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
            mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(true)).Returns(Task.FromResult(false));

            _mockCollection
                .Setup(c => c.FindAsync(
                    It.IsAny<FilterDefinition<GameItem>>(),
                    It.IsAny<FindOptions<GameItem, GameItem>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, g => g.Title == "Game 1");
            Assert.Contains(result, g => g.Title == "Game 2");
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectGame()
        {
            // Arrange
            var game = new GameItem { Id = "123", Title = "Game 123" };

            var mockCursor = new Mock<IAsyncCursor<GameItem>>();
            mockCursor.Setup(_ => _.Current).Returns(new List<GameItem> { game });
            mockCursor.SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
            mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(true)).Returns(Task.FromResult(false));

            _mockCollection
                .Setup(c => c.FindAsync(It.IsAny<FilterDefinition<GameItem>>(), It.IsAny<FindOptions<GameItem, GameItem>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _service.GetByIdAsync("123");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Game 123", result.Title);
        }

        [Fact]
        public async Task GetByDeveloperIdAsync_ReturnsGamesForDeveloper()
        {
            // Arrange
            var developerId = "dev1";
            var games = new List<GameItem>
            {
                new GameItem { Id = "1", DeveloperId = developerId, Title = "Game A" },
                new GameItem { Id = "2", DeveloperId = developerId, Title = "Game B" }
            };

            var mockCursor = new Mock<IAsyncCursor<GameItem>>();
            mockCursor.Setup(_ => _.Current).Returns(games);
            mockCursor.SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
            mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(true)).Returns(Task.FromResult(false));

            _mockCollection
                .Setup(c => c.FindAsync(It.IsAny<FilterDefinition<GameItem>>(), It.IsAny<FindOptions<GameItem, GameItem>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _service.GetByDeveloperIdAsync(developerId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, g => Assert.Equal(developerId, g.DeveloperId));
        }

        [Fact]
        public async Task GetByGenreAsync_ReturnsCorrectGenre()
        {
            // Arrange
            var games = new List<GameItem>
            {
                new GameItem { Id = "1", Genre = GameItem.GenreType.RPG },
                new GameItem { Id = "2", Genre = GameItem.GenreType.RPG },
            };

            var mockCursor = new Mock<IAsyncCursor<GameItem>>();
            mockCursor.Setup(_ => _.Current).Returns(games);
            mockCursor.SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
            mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(true)).Returns(Task.FromResult(false));

            _mockCollection
                .Setup(c => c.FindAsync(It.IsAny<FilterDefinition<GameItem>>(), It.IsAny<FindOptions<GameItem, GameItem>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _service.GetByGenreAsync("RPG");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, g => Assert.Equal(GameItem.GenreType.RPG, g.Genre));
        }

        [Fact]
        public async Task CreateAsync_CallsInsertOneAsync()
        {
            // Arrange
            var game = new GameItem { Id = "1", Title = "New Game" };

            // Act
            await _service.CreateAsync(game);

            // Assert
            _mockCollection.Verify(c => c.InsertOneAsync(game, null, default), Times.Once);
        }


        [Fact]
        public async Task DeleteAsync_CallsDeleteOneAsync()
        {
            // Act
            await _service.DeleteAsync("1");

            // Assert
            _mockCollection.Verify(c => c.DeleteOneAsync(It.IsAny<FilterDefinition<GameItem>>(), default), Times.Once);
        }
    }
}
