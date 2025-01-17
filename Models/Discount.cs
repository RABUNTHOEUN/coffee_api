﻿namespace thoeun_coffee.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public int? ProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }

        public Product? Product { get; set; }
    }
}
