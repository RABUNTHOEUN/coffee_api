using Microsoft.AspNetCore.Mvc;
using thoeun_coffee.Data;
using thoeun_coffee.Models;

namespace thoeun_coffee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeBeanController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoffeeBeanController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CoffeeBean
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoffeeBean>>> GetCoffeeBeans()
        {
            // Fetch CoffeeBeans and order them by BeanId in descending order (latest first)
            var coffeeBeans = await _context.CoffeeBeans
                .OrderByDescending(c => c.BeanId)  // Order by BeanId in descending order
                .ToListAsync();

            // Return the ordered list of CoffeeBeans
            return Ok(coffeeBeans);
        }



        // GET: api/CoffeeBean/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoffeeBean>> GetCoffeeBean(int id)
        {
            var coffeeBean = await _context.CoffeeBeans.FindAsync(id);

            if (coffeeBean == null)
            {
                return NotFound();
            }

            return coffeeBean;
        }

        // POST: api/CoffeeBean
        [HttpPost]
        public async Task<ActionResult<CoffeeBean>> PostCoffeeBean(CoffeeBean coffeeBean)
        {
            _context.CoffeeBeans.Add(coffeeBean);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoffeeBean", new { id = coffeeBean.BeanId }, coffeeBean);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoffeeBean(int id, CoffeeBean coffeeBean)
        {
            if (id != coffeeBean.BeanId)
            {
                return BadRequest(new { message = "Id in URL does not match Id in payload." });
            }

            var existingCoffeeBean = await _context.CoffeeBeans.FindAsync(id);
            if (existingCoffeeBean == null)
            {
                return NotFound(new { message = "CoffeeBean not found." });
            }

            // Update properties manually to avoid overposting issues
            existingCoffeeBean.Name = coffeeBean.Name;
            existingCoffeeBean.Origin = coffeeBean.Origin;
            existingCoffeeBean.RoastLevel = coffeeBean.RoastLevel;
            existingCoffeeBean.PricePerKg = coffeeBean.PricePerKg;
            existingCoffeeBean.StockQuantity = coffeeBean.StockQuantity;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Concurrency conflict occurred. Try again." });
            }

            return NoContent();
        }


        // DELETE: api/CoffeeBean/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoffeeBean(int id)
        {
            var coffeeBean = await _context.CoffeeBeans.FindAsync(id);
            if (coffeeBean == null)
            {
                return NotFound();
            }

            _context.CoffeeBeans.Remove(coffeeBean);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoffeeBeanExists(int id)
        {
            return _context.CoffeeBeans.Any(e => e.BeanId == id);
        }
    }
}
