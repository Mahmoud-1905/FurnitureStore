using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; } = string.Empty;

        [Required]
        public decimal DiscountValue { get; set; }

        public bool IsPercentage { get; set; } = true; // true = %, false = fixed amount

        public decimal? MaxDiscountAmount { get; set; } // for percentage coupons

        public decimal? MinimumOrderAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? UsageLimit { get; set; }

        public int TimesUsed { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
