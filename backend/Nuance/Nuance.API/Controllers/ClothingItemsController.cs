using Microsoft.AspNetCore.Mvc;
using Nuance.API.Models;
using Nuance.API.Services.Interfaces;
using Nuance.API.DTOs;

namespace Nuance.API.Controllers
{

        [ApiController]
        [Route("api/clothing-items")]
        public class  ClothingItemsController : ControllerBase
        {
            private readonly IClothingItemService _service;
            
            public ClothingItemsController(IClothingItemService service)
            {

                _service = service;

            }

        [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var items = await _service.GetAllAsync();
                return Ok(items);
            }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
            public async Task<IActionResult> Add(
                [FromBody] CreateClothingItemDTO dto)
            {
                var item = new ClothingItem
                {
                    Name = dto.Name,
                    Category = dto.Category,
                    Color = dto.Color,
                    Material = dto.Material,
                    Size = dto.Size
                };
            var createdItem = await _service.AddAsync(item);
                
                return StatusCode(201, createdItem);
            }

        [HttpPut("{id:int}")]
            public async Task<IActionResult> Update(int id, [FromBody] UpdateClothingItemDTO dto)
            {
                var updatedItem = await _service.UpdateAsync(id, dto);
                if (updatedItem == null)
                {
                    return NotFound();
                }
                return Ok(updatedItem);
            }
        [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete(int id)
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
        }
}