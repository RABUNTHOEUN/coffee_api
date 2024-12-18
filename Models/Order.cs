﻿using System.Text.Json.Serialization;

namespace thoeun_coffee.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? PaymentId { get; set; }
        public string OrderStatus { get; set; } // 'pending', 'completed', 'cancelled'
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;

        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public Payment? Payment { get; set; }
        // [JsonIgnore]
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
