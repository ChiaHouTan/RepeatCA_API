using GamesAPI.Models;
using GamesAPI.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<GameItem>>> Get() =>
            await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<GameItem>> Get(string id)
        {
            var game = await _service.GetByIdAsync(id);
            if (game is null) return NotFound();
            return game;
        }

        [HttpGet("byDeveloper/{developerId}")]
        public async Task<ActionResult<List<GameItem>>> GetByDeveloper(string developerId) =>
            await _service.GetByDeveloperIdAsync(developerId);

        [HttpPost]
        public async Task<IActionResult> Post(GameItem game)
        {
            await _service.CreateAsync(game);
            return CreatedAtAction(nameof(Get), new { id = game.Id }, game);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, GameItem game)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing is null) return NotFound();

            game.Id = existing.Id;
            await _service.UpdateAsync(id, game);
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
