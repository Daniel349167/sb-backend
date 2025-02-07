using Microsoft.AspNetCore.Mvc;
using SB.Prueba.Application.Interfaces;
using SB.Prueba.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace SB.Prueba.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GovernmentEntityController : ControllerBase
    {
        private readonly IGovernmentEntityService _service;

        public GovernmentEntityController(IGovernmentEntityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GovernmentEntity entity)
        {
            await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GovernmentEntity entity)
        {
            if (id != entity.Id)
                return BadRequest("ID no coincide");

            var existingEntity = await _service.GetByIdAsync(id);
            if (existingEntity == null)
                return NotFound();

            await _service.UpdateAsync(entity);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
