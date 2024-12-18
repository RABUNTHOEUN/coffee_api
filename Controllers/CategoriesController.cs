using coffee_api.Dtos.Category;
using coffee_api.Mappers;
using Microsoft.AspNetCore.Mvc;
using thoeun_coffee.Data;
using thoeun_coffee.Models;

namespace thoeun_coffee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .Select(c => c.ToCategoryDto())
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound(new { Message = $"Category with ID {id} not found." });
            }

            return Ok(category.ToCategoryDto());
        }

        // POST: api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            // Create a new category entity from the DTO
            var category = new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description
            };

            // Add the category to the database
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Prepare the response with a message and the created category data
            var response = new
            {
                Message = "Category created successfully.",
                Data = category.ToCategoryDto()
            };

            // Return a 201 Created response with the custom message and data
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, response);
        }


        // PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            // Find the category by ID
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            // Check if the category exists
            if (category == null)
            {
                return NotFound(new { Message = $"Category with ID {id} not found." });
            }

            // Update entity properties
            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a response with a success message and the updated data
            var response = new
            {
                Message = $"Category with ID {id} updated successfully.",
                Data = category.ToCategoryDto() // Convert to DTO for the response
            };

            return Ok(response);
        }


        // DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound(new { Message = $"Category with ID {id} not found." });
            }

            if (category.Products.Any())
            {
                return BadRequest(new { Message = "Cannot delete a category with associated products." });
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Category with ID {id} deleted successfully." });
        }

        // Private method to check if a category exists
        private async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(e => e.Id == id);
        }
    }
}
