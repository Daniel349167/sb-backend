// Archivo: GovernmentEntityController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SB.Prueba.Application.Interfaces;
using SB.Prueba.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GovernmentEntity>))]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GovernmentEntity))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GovernmentEntity))]
        public async Task<IActionResult> Create(GovernmentEntity entity)
        {
            await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
