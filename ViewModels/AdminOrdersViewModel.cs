using System.Collections.Generic;
using FurnitureStore.Models;

namespace FurnitureStore.ViewModels
{
    public class AdminOrdersViewModel
    {
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
        public OrderStatus? FilterStatus { get; set; }
    }
}
