using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(4000)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; } = 0;

        [Required]
        [MaxLength(50)]
        public string SKU { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Material { get; set; }

        /// <summary>Width in cm</summary>
        public decimal? DimensionW { get; set; }

        /// <summary>Depth in cm</summary>
        public decimal? DimensionD { get; set; }

        /// <summary>Height in cm</summary>
        public decimal? DimensionH { get; set; }

        /// <summary>For shipping calculation</summary>
        public decimal? WeightKg { get; set; }

        public bool IsActive { get; set; } = true;

        /// <summary>Homepage featured section</summary>
        public bool IsFeatured { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Legacy field kept for backward compatibility with existing seed data.
        /// New code should use CategoryId FK instead.
        /// </summary>
        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        // Navigation properties
        public Category Category { get; set; } = null!;
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
