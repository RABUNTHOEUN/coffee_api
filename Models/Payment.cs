namespace thoeun_coffee.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } // 'credit_card', 'cash', 'paypal'
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Order Order { get; set; }
    }
}
