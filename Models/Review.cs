using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public int ProductId { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; } = string.Empty;

        [Range(1, 5)]
        public byte Rating { get; set; }

        [MaxLength(150)]
        public string? Title { get; set; }

        [MaxLength(2000)]
        public string? Body { get; set; }

        /// <summary>
        /// System-set: requires Delivered OrderItem
        /// </summary>
        public bool IsVerifiedPurchase { get; set; } = false;

        /// <summary>
        /// Admin moderation toggle
        /// </summary>
        public bool IsVisible { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Product Product { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
