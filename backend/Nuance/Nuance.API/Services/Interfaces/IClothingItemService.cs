using Nuance.API.Models;

namespace Nuance.API.Services.Interfaces
{
    public interface IClothingItemService
    {
        IEnumerable<ClothingItem> GetAll();

        ClothingItem Add(ClothingItem item);
    }
}
