using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        // Foreign key to ApplicationUser
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(450)]
        // Optional coupon applied to the cart
        public int? CouponId { get; set; }
        public Coupon? Coupon { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ApplicationUser User { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
