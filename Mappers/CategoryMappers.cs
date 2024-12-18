using coffee_api.Dtos.Category;
using coffee_api.Dtos.Product;
using thoeun_coffee.Models;

namespace coffee_api.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDto ToCategoryDto(this Category categoryModel)
        {
            return new CategoryDto
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name,
                Description = categoryModel.Description,
                Products = categoryModel.Products?.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = categoryModel.Name, // Explicitly map CategoryName from parent
                }).ToList() // Map Products to ProductDto
            };
        }
    }
}
