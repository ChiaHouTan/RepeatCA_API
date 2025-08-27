using GamesAPI.Models;
using GamesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly GameService _service;

        public GamesController(GameService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameDto>>> Get()
        {
            var games = await _service.GetAllAsync();

            var result = games.Select(g => new GameDto
            {
                Id = g.Id,
                Title = g.Title,
                Image = g.Image,
                ReleaseDate = g.ReleaseDate,
                Genre = g.Genre.ToString(),
                Price = g.Price,
                Rating = g.Rating,
                DeveloperId = g.DeveloperId
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> Get(string id)
        {
            var g = await _service.GetByIdAsync(id);
            if (g is null) return NotFound();

            var result = new GameDto
            {
                Id = g.Id,
                Title = g.Title,
                Image = g.Image,
                ReleaseDate = g.ReleaseDate,
                Genre = g.Genre.ToString(),
                Price = g.Price,
                Rating = g.Rating,
                DeveloperId = g.DeveloperId
            };

            return Ok(result);
        }

        [HttpGet("byDeveloper/{developerId}")]
        public async Task<ActionResult<List<GameDto>>> GetByDeveloper(string developerId)
        {
            var games = await _service.GetByDeveloperIdAsync(developerId);

            var result = games.Select(g => new GameDto
            {
                Id = g.Id,
                Title = g.Title,
                Image = g.Image,
                ReleaseDate = g.ReleaseDate,
                Genre = g.Genre.ToString(),
                Price = g.Price,
                Rating = g.Rating,
                DeveloperId = g.DeveloperId
            }).ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(GameDto dto)
        {
            var game = new GameItem
            {
                Title = dto.Title,
                Image = dto.Image,
                ReleaseDate = dto.ReleaseDate,
                Genre = Enum.Parse<GameItem.GenreType>(dto.Genre!, true), // string → enum, ignore case
                Price = dto.Price,
                Rating = dto.Rating,
                DeveloperId = dto.DeveloperId
            };

            await _service.CreateAsync(game);

            // return formatted output
            var result = new GameDto
            {
                Id = game.Id.ToString(),
                Title = game.Title,
                Image = game.Image,
                ReleaseDate = game.ReleaseDate,
                Genre = game.Genre.ToString(),
                Price = game.Price,
                Rating = game.Rating,
                DeveloperId = game.DeveloperId.ToString()
            };

            return CreatedAtAction(nameof(Get), new { id = game.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, GameDto dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing is null) return NotFound();

            existing.Title = dto.Title;
            existing.Image = dto.Image;
            existing.ReleaseDate = dto.ReleaseDate;
            existing.Genre = Enum.Parse<GameItem.GenreType>(dto.Genre!, true);
            existing.Price = dto.Price;
            existing.Rating = dto.Rating;
            existing.DeveloperId = dto.DeveloperId;

            await _service.UpdateAsync(id, existing);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing is null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
