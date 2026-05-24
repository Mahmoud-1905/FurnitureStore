using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public enum OrderStatus
    {
        Pending,
        Paid,
        Shipped,
        Completed,
        Cancelled
    }

    public enum PaymentStatus
    {
        Pending,
        Succeeded,
        Failed,
        Refunded
    }
}
