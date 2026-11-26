using Nuance.API.Services.Interfaces;
using Nuance.API.Models;
using Nuance.API.Data;
using Nuance.API.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Nuance.API.Services
{
    public class ClothingItemService : IClothingItemService
    {
        private readonly NuanceDbContext _context;
        public ClothingItemService(NuanceDbContext context)
        {
            _context = context;
        }
        public async Task<List<ClothingItem>> GetAllAsync()
        {
            return await _context.ClothingItems.ToListAsync();
        }

        public async Task<ClothingItem> AddAsync(ClothingItem item, int wardrobeId)
        {
            var wardrobe = await _context.Wardrobes.FindAsync(wardrobeId);
            if (wardrobe == null)
            {
                throw new InvalidOperationException("Wardrobe not found.");
            }

            item.WardrobeId = wardrobeId;
            item.CreatedAt = DateTime.UtcNow;

            _context.ClothingItems.Add(item);
            await _context.SaveChangesAsync();

            return item;
        }
        public async Task<ClothingItem?> GetByIdAsync(int id)
        {
            return await _context.ClothingItems.FindAsync(id);
        }
        public async Task<ClothingItem?> UpdateAsync(int id, UpdateClothingItemDTO dto, string userId)
        {
            var item = await _context.ClothingItems.FirstOrDefaultAsync(c =>
            c.Id == id &&
            c.Wardrobe != null &&
            c.Wardrobe.UserId == userId);

            if (item == null)
            {
                return null;
            }
            item.Name = dto.Name;
            item.Color = dto.Color;
            item.Size = dto.Size;
            item.Category = dto.Category;
            item.Material = dto.Material;

            await _context.SaveChangesAsync();

            return item;
        }
        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var item = await _context.ClothingItems.FirstOrDefaultAsync(c =>
                c.Id == id &&
                c.Wardrobe != null &&
                c.Wardrobe.UserId == userId);
            if (item == null)
            {
                return false;
            }
            _context.ClothingItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<ClothingItem>> GetByWardrobeIdAsync(int wardrobeId)
        {
            return await _context.ClothingItems.Where(item => item.WardrobeId == wardrobeId).ToListAsync();
        }
    }
}
