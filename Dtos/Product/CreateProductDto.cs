using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coffee_api.Dtos.Discount;

namespace coffee_api.Dtos.Product
{
    public class CreateProductDto
    {

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }  // Removed duplicate declaration
        public int? InventoryId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public List<DiscountDto> Discounts { get; set; } 
    }
}