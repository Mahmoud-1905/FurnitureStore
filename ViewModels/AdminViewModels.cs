using FurnitureStore.Models;

namespace FurnitureStore.ViewModels
{
    public class DashboardOverviewViewModel
    {
        public int TotalVisitors { get; set; }
        public int TotalPageViews { get; set; }
        public double BounceRate { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int LowStockProductsCount { get; set; }
        
        // Mock revenue chart data
        public List<string> ChartLabels { get; set; } = new List<string>();
        public List<decimal> ChartData { get; set; } = new List<decimal>();
    }

    public class AdminOrdersViewModel
    {
        public List<Order> Orders { get; set; } = new List<Order>();
        public OrderStatus? FilterStatus { get; set; }
    }

    public class AdminPaymentsViewModel
    {
        public List<Order> UnpaidOrders { get; set; } = new List<Order>();
    }

    public class AdminInventoryViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
    }

    public class AdminCustomersViewModel
    {
        public List<ApplicationUser> Customers { get; set; } = new List<ApplicationUser>();
    }

    public class AdminReviewsViewModel
    {
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
