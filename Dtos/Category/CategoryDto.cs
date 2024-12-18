using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coffee_api.Dtos.Product;
using thoeun_coffee.Models;

namespace coffee_api.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;

        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}