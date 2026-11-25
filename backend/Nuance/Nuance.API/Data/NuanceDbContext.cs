using Microsoft.EntityFrameworkCore;
using Nuance.API.Models;

namespace Nuance.API.Data
{
    public class NuanceDbContext : DbContext
    {
        public NuanceDbContext(DbContextOptions<NuanceDbContext> options) : base(options)
        {
        }

        public DbSet<ClothingItem> ClothingItems => Set<ClothingItem>();
    }
}
