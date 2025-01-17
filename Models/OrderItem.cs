﻿using System.Text.Json.Serialization;

namespace thoeun_coffee.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
        // [JsonIgnore]
        public Product? Product { get; set; }
    }
}
