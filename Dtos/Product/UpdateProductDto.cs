using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coffee_api.Dtos.Discount;

namespace coffee_api.Dtos.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public List<DiscountDto> Discounts { get; set; } 
    }
}