using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class ProductImage
    {
        public int ImageId { get; set; }

        public int ProductId { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsPrimary { get; set; } = false;

        public int SortOrder { get; set; } = 0;

        // Navigation property
        public Product Product { get; set; } = null!;
    }
}
