using System.ComponentModel.DataAnnotations;

namespace Nuance.API.DTOs
{
    public class CreateClothingItemDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        public string Color { get; set; } = string.Empty;
        [Required]
        public string Material { get; set; } = string.Empty;
        [Required]
        public string Size { get; set; } = string.Empty;
    }
}
