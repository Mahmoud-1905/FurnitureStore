using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        public int SortOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        // Navigation property
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
