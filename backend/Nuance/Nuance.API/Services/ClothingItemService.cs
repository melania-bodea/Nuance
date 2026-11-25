using Nuance.API.Services.Interfaces;
using Nuance.API.Models;
using Nuance.API.Data;
using Nuance.API.DTOs;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ClothingItem> AddAsync(ClothingItem item)
        {
            _context.ClothingItems.Add(item);
            await _context.SaveChangesAsync();

            return item;
        }
        public async Task<ClothingItem?> GetByIdAsync(int id)
        {
            return await _context.ClothingItems.FindAsync(id);
        }
        public async Task<ClothingItem?> UpdateAsync(int id, UpdateClothingItemDTO dto)
        {
            var item = await _context.ClothingItems.FindAsync(id);
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
        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.ClothingItems.FindAsync(id);
            if (item == null)
            {
                return false;
            }
            _context.ClothingItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
