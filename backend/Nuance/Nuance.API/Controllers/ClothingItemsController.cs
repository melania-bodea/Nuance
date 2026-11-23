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
            public ActionResult<IEnumerable<ClothingItem>> GetAll()
            {
                return Ok(_service.GetAll());
            }

        [HttpPost]
            public ActionResult<ClothingItem> Add(
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
            var createdItem = _service.Add(item);
                
                return StatusCode(201, createdItem);
            }
        }
}