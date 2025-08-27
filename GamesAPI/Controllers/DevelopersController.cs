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
            var devs = await _service.GetAllAsync();
            return devs.Select(d => new DeveloperDto
            {
                Id = d.Id.ToString(),
                DeveloperName = d.DeveloperName,
                FoundedYear = d.FoundedYear.Year,
                Country = d.Country,
                IsIndependent = d.IsIndependent
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeveloperDto>> Get(string id)
        {
            var dev = await _service.GetByIdAsync(id);
            if (dev is null) return NotFound();

            return new DeveloperDto
            {
                Id = dev.Id.ToString(),
                DeveloperName = dev.DeveloperName,
                FoundedYear = dev.FoundedYear.Year,
                Country = dev.Country,
                IsIndependent = dev.IsIndependent
            };
        }

        [HttpPost]
        public async Task<IActionResult> Post(DeveloperDto dto)
        {
            var dev = new DeveloperItem
            {
                DeveloperName = dto.DeveloperName,
                FoundedYear = new DateTime(dto.FoundedYear, 1, 1),
                Country = dto.Country,
                IsIndependent = dto.IsIndependent
            };

            await _service.CreateAsync(dev);

            return CreatedAtAction(nameof(Get), new { id = dev.Id }, new DeveloperDto
            {
                Id = dev.Id.ToString(),
                DeveloperName = dev.DeveloperName,
                FoundedYear = dev.FoundedYear.Year,
                Country = dev.Country,
                IsIndependent = dev.IsIndependent
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, DeveloperDto dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing is null) return NotFound();

            existing.DeveloperName = dto.DeveloperName;
            existing.FoundedYear = new DateTime(dto.FoundedYear, 1, 1);
            existing.Country = dto.Country;
            existing.IsIndependent = dto.IsIndependent;

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
