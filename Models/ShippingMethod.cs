using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class ShippingMethod
    {
        public int ShippingMethodId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Cost { get; set; }

        // Estimated delivery days (e.g., 3-5 days)
        public int EstimatedDays { get; set; }
    }
}
