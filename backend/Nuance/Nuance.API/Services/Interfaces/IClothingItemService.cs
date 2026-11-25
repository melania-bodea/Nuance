using Nuance.API.Models;
using Nuance.API.DTOs;

namespace Nuance.API.Services.Interfaces
{
    public interface IClothingItemService
    {
        Task<List<ClothingItem>> GetAllAsync();
        Task<ClothingItem> AddAsync(ClothingItem item);
        Task<ClothingItem?> GetByIdAsync(int id);
        Task<ClothingItem?> UpdateAsync(int id, UpdateClothingItemDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
