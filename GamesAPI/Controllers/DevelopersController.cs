using GamesAPI.Models;
using GamesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevelopersController : ControllerBase
    {
        private readonly DeveloperService _service;

        public DevelopersController(DeveloperService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeveloperDto>>> Get()
        {

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeveloperItem>> Get(string id)
        {
            var dev = await _service.GetByIdAsync(id);
            if (dev is null) return NotFound();
            return dev;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DeveloperItem dev)
        {
            await _service.CreateAsync(dev);
            return CreatedAtAction(nameof(Get), new { id = dev.Id }, dev);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, DeveloperItem dev)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing is null) return NotFound();

            dev.Id = existing.Id;
            await _service.UpdateAsync(id, dev);
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
