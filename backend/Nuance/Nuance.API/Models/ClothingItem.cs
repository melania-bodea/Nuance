namespace Nuance.API.Models
{
    public class ClothingItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
