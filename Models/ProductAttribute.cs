using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class ProductAttribute
    {
        public int AttributeId { get; set; }

        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Key { get; set; } = string.Empty; // e.g., "Wood Type", "Upholstery"

        [Required]
        [MaxLength(500)]
        public string Value { get; set; } = string.Empty; // e.g., "Solid Oak", "Premium Velvet"

        // Navigation property
        public Product Product { get; set; } = null!;
    }
}
