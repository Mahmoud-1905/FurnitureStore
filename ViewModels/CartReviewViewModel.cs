using System;
using System.Collections.Generic;
using FurnitureStore.Models;

namespace FurnitureStore.ViewModels
{
    public class CartReviewViewModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal SubTotal { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
    }
}
