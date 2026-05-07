namespace FurnitureStore.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        /// <summary>
        /// SNAPSHOT — never references live Product.Price
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Qty × UnitPrice; stored for query performance
        /// </summary>
        public decimal Subtotal { get; set; }

        // Navigation properties
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
