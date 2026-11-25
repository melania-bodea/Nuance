using System.ComponentModel.DataAnnotations;

namespace Nuance.API.DTOs
{
    public class UpdateClothingItemDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Material { get; set; }
    }
}
