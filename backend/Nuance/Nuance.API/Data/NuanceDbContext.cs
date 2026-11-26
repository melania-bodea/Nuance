using Microsoft.EntityFrameworkCore;
using Nuance.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Nuance.API.Data
{
    public class NuanceDbContext : IdentityDbContext<ApplicationUser>
    {
        public NuanceDbContext(DbContextOptions<NuanceDbContext> options) : base(options)
        {
        }

        public DbSet<ClothingItem> ClothingItems => Set<ClothingItem>();
        public DbSet<Wardrobe> Wardrobes => Set<Wardrobe>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<ApplicationUser>()
                    .HasOne(user => user.Wardrobe)
                    .WithOne(wardrobe => wardrobe.User)
                    .HasForeignKey<Wardrobe>(wardrobe => wardrobe.UserId);
            }
        }
}