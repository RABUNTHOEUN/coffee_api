using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coffee_api.Dtos.Discount
{
    public class DiscountDto
    {
        public int DiscountId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
    }
}