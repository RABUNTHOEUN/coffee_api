using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coffee_api.Dtos.Order
{
    public class EditOrderDto
    {
        public int? UserId { get; set; }
        public int? PaymentId { get; set; }
        public string? OrderStatus { get; set; } // 'pending', 'completed', 'cancelled'
        public decimal? TotalAmount { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? DeliveryAddress { get; set; }
        public List<EditOrderItemDto>? OrderItems { get; set; }
    }
}