using Nuance.API.Models;
using Nuance.API.DTOs;

namespace Nuance.API.Services.Interfaces
{
    public interface IClothingItemService
    {
        Task<List<ClothingItem>> GetAllAsync();
        Task<ClothingItem> AddAsync(ClothingItem item, int wardrobeId);
        Task<ClothingItem?> GetByIdAsync(int id);
        Task<ClothingItem?> UpdateAsync(int id, UpdateClothingItemDTO dto, string userId);
        Task<bool> DeleteAsync(int id, string userId);
        Task<IEnumerable<ClothingItem>> GetByWardrobeIdAsync(int wardrobeId);
    }
}
