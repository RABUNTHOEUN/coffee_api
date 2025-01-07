using coffee_api.Dtos.Discount;
using coffee_api.Dtos.Product;
using Microsoft.AspNetCore.Mvc;
using thoeun_coffee.Data;
using thoeun_coffee.Models;

namespace thoeun_coffee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)  // Include Category
                .Include(p => p.Discounts)  // Include Discounts
                .ToListAsync();

            var productDtos = products.OrderByDescending(p => p.CreatedAt).Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name,
                Discounts = p.Discounts.Select(d => new DiscountDto
                {
                    DiscountId = d.DiscountId,
                    Code = d.Code,
                    Description = d.Description,
                    DiscountPercentage = d.DiscountPercentage,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    Active = d.Active
                }).ToList()  // Map Discounts to DiscountDto
            });

            return Ok(productDtos);
        }


        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)  // Include Category
                .Include(p => p.Discounts)  // Include Discounts
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                Discounts = product.Discounts.Select(d => new DiscountDto
                {
                    DiscountId = d.DiscountId,
                    Code = d.Code,
                    Description = d.Description,
                    DiscountPercentage = d.DiscountPercentage,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    Active = d.Active
                }).ToList()  // Map Discounts to DiscountDto
            };

            return Ok(productDto);
        }


        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            if (createProductDto == null)
            {
                return BadRequest("Product data is required.");
            }

            var product = new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                CategoryId = createProductDto.CategoryId,
                Discounts = createProductDto.Discounts.Select(d => new Discount
                {
                    Code = d.Code,
                    Description = d.Description,
                    DiscountPercentage = d.DiscountPercentage,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    Active = d.Active
                }).ToList()  // Mapping the discount data
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }


        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            var product = await _context.Products.Include(p => p.Discounts).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            // Update product properties
            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.CategoryId = updateProductDto.CategoryId;

            // You can add logic to update or remove discounts if necessary
            if (updateProductDto.Discounts != null)
            {
                product.Discounts = updateProductDto.Discounts.Select(d => new Discount
                {
                    DiscountId = d.DiscountId,
                    Code = d.Code,
                    Description = d.Description,
                    DiscountPercentage = d.DiscountPercentage,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    Active = d.Active
                }).ToList();
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = $"Product with ID {id} updated successfully.",
                product
            });
        }


        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Product with ID {id} deleted successfully." });
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
