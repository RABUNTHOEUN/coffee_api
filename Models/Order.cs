namespace thoeun_coffee.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string OrderStatus { get; set; } // 'pending', 'completed', 'cancelled'
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; }

        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
