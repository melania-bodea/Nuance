using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nuance.API.Data;
using Nuance.API.DTOs;
using Nuance.API.Models;
using Nuance.API.Services;
using Nuance.API.Services.Interfaces;
using System.Security.Claims;

namespace Nuance.API.Controllers
{

        [ApiController]
        [Route("api/clothing-items")]
        [Authorize]
        public class  ClothingItemsController : ControllerBase
        {
            private readonly IClothingItemService _service;
            private readonly NuanceDbContext _context;

        public ClothingItemsController(IClothingItemService service, NuanceDbContext context)
            {

                _service = service;
                _context = context;

            }

        [HttpGet]
            public async Task<IActionResult> GetAll()
            {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var wardrobe = await _context.Wardrobes.FirstOrDefaultAsync(w => w.UserId == userId);

            if (wardrobe == null)
                return NotFound("Wardrobe not found.");

            var items = await _service.GetByWardrobeIdAsync(wardrobe.Id);

            var response = items.Select(item => new ClothingItemResponseDTO
            {
                Id = item.Id,
                Name = item.Name,
                Category = item.Category,
                Color = item.Color,
                Material = item.Material,
                Size = item.Size,
                WardrobeId = item.WardrobeId,
                CreatedAt = item.CreatedAt
            }).ToList();

            return Ok(response);
        }
        [HttpGet("{id:int}")]
            public async Task<IActionResult> GetById(int id)
            {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var item = await _context.ClothingItems
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.Wardrobe != null &&
                    c.Wardrobe.UserId == userId);
            if (item == null)
            {
                return NotFound();
            }
            var response = new ClothingItemResponseDTO
            {
                Id = item.Id,
                Name = item.Name,
                Category = item.Category,
                Color = item.Color,
                Material = item.Material,
                Size = item.Size,
                WardrobeId = item.WardrobeId,
                CreatedAt = item.CreatedAt
            };

            return Ok(response);
        }

        [HttpPost]
            public async Task<IActionResult> Add([FromBody] CreateClothingItemDTO dto)
            {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var wardrobe = await _context.Wardrobes.FirstOrDefaultAsync(w => w.UserId == userId);

            if (wardrobe == null)
                return NotFound("Wardrobe not found.");

            var item = new ClothingItem
                {
                    Name = dto.Name,
                    Category = dto.Category,
                    Color = dto.Color,
                    Material = dto.Material,
                    Size = dto.Size
                };
            var createdItem = await _service.AddAsync(item, wardrobe.Id);

            var response = new ClothingItemResponseDTO
            {
                Id = createdItem.Id,
                Name = createdItem.Name,
                Category = createdItem.Category,
                Color = createdItem.Color,
                Material = createdItem.Material,
                Size = createdItem.Size,
                WardrobeId = createdItem.WardrobeId,
                CreatedAt = createdItem.CreatedAt
            };

            return StatusCode(201, response);
        }

        [HttpPut("{id:int}")]
            public async Task<IActionResult> Update(int id, [FromBody] UpdateClothingItemDTO dto)
            {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var item = await _context.ClothingItems
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.Wardrobe != null &&
                    c.Wardrobe.UserId == userId);

            if (item == null)
                return NotFound();
            var updatedItem = await _service.UpdateAsync(id, dto, userId);
                if (updatedItem == null)
                {
                    return NotFound();
                }
                return Ok(updatedItem);
            }
        [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete(int id)
            {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();
            var deleted = await _service.DeleteAsync(id, userId);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
        }
}