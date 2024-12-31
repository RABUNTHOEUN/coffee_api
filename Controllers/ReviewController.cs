using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thoeun_coffee.Data;
using thoeun_coffee.Models;

namespace thoeun_coffee.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ReviewController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Review
        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _dbContext.Reviews
                                          .Include(r => r.User)
                                          .Include(r => r.Product)
                                          .ToListAsync();
            return Ok(reviews);
        }

        // GET: api/Review/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _dbContext.Reviews
                                         .Include(r => r.User)
                                         .Include(r => r.Product)
                                         .FirstOrDefaultAsync(r => r.ReviewId == id);
            if (review == null)
            {
                return NotFound(new { message = "Review not found." });
            }
            return Ok(review);
        }

        // POST: api/Review
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] Review newReview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Reviews.Add(newReview);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviewById), new { id = newReview.ReviewId }, newReview);
        }

        // PUT: api/Review/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] Review updatedReview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingReview = await _dbContext.Reviews.FindAsync(id);
            if (existingReview == null)
            {
                return NotFound(new { message = "Review not found." });
            }

            existingReview.Rating = updatedReview.Rating;
            existingReview.ReviewText = updatedReview.ReviewText;
            existingReview.ReviewDate = updatedReview.ReviewDate;
            existingReview.UserId = updatedReview.UserId;
            existingReview.ProductId = updatedReview.ProductId;

            await _dbContext.SaveChangesAsync();
            return Ok(existingReview);
        }

        // DELETE: api/Review/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _dbContext.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound(new { message = "Review not found." });
            }

            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Review deleted successfully." });
        }
    }
}
