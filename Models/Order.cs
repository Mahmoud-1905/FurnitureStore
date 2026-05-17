using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Pending";

        public decimal TotalAmount { get; set; }

        public decimal ShippingCost { get; set; }

        public decimal TaxAmount { get; set; }

        [Required]
        [MaxLength(600)]
        public string DeliveryAddress { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Notes { get; set; }

        [Required]
        [MaxLength(20)]
        public string PaymentStatus { get; set; } = "Unpaid";

        [MaxLength(50)]
        public string? PaymentMethod { get; set; }

        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Coupon integration
        public int? CouponId { get; set; }
        public decimal DiscountAmount { get; set; } = 0;

        // Address integration (reference to saved address)
        public int? AddressId { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Coupon? Coupon { get; set; }
        public Payment? Payment { get; set; }
        public Address? Address { get; set; }
    }
}
