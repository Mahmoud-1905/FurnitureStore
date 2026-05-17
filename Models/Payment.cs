using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int OrderId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty; // Stripe, PayPal, COD, etc.

        [Required]
        [MaxLength(100)]
        public string TransactionId { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Completed, Failed, Refunded

        public string? ProviderResponse { get; set; } // JSON response from gateway

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public Order Order { get; set; } = null!;
    }
}
