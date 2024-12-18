using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coffee_api.Dtos.Product;
using thoeun_coffee.Models;

namespace coffee_api.Mappers
{
    public static class ProductMappers
    {
        public static Product ToProductFromCreateDTO(this CreateProductDto productDto){
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                

            };
        }
    }
}