using System;
using System.Collections.Generic;
using FurnitureStore.Models;

namespace FurnitureStore.ViewModels
{
    public class OrderConfirmationViewModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        public Address Address { get; set; }
        public int? CouponId { get; set; }
    }
}
