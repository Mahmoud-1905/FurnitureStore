using FurnitureStore.Models;

namespace FurnitureStore.ViewModels
{
    public class OrderViewModel 
    {
        public int OrderId { get; set; }

        public string? DeliveryAddress { get; set; }

        public decimal TotalAmount { get; set; }

        // Discount applied to the order
        public decimal DiscountAmount { get; set; } = 0m;

        public decimal ShippingCost { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
