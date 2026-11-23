using Nuance.API.Services.Interfaces;
using Nuance.API.Models;

namespace Nuance.API.Services
{
    public class ClothingItemService : IClothingItemService
    {
        private readonly List<ClothingItem> _items = new();
        private int _nextId = 1;
        public IEnumerable<ClothingItem> GetAll()
        {
            return _items;
        }

        public ClothingItem Add(ClothingItem item)
        {
            item.Id = _nextId++;
            _items.Add(item);
            return item;
        }
    }
}
