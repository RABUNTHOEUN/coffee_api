using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thoeun_coffee.Data;
using thoeun_coffee.Models;

namespace thoeun_coffee.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffShiftController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public StaffShiftController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/StaffShift
        [HttpGet]
        public async Task<IActionResult> GetAllShifts()
        {
            var shifts = await _dbContext.StaffShifts.Include(s => s.User).ToListAsync();
            return Ok(shifts);
        }

        // GET: api/StaffShift/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShiftById(int id)
        {
            var shift = await _dbContext.StaffShifts.Include(s => s.User).FirstOrDefaultAsync(s => s.ShiftId == id);
            if (shift == null)
            {
                return NotFound(new { success = false, message = "Shift not found." });
            }
            return Ok(shift);
        }

        // POST: api/StaffShift
        [HttpPost]
        public async Task<IActionResult> CreateShift([FromBody] StaffShift newShift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid data.", errors = ModelState.Values });
            }

            if (!newShift.IsValidShift())
            {
                return BadRequest(new { success = false, message = "Start time must be before end time." });
            }

            if (!newShift.IsShiftInFuture())
            {
                return BadRequest(new { success = false, message = "Shift date cannot be in the past." });
            }

            _dbContext.StaffShifts.Add(newShift);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShiftById), new { id = newShift.ShiftId }, newShift);
        }

        // PUT: api/StaffShift/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShift(int id, [FromBody] StaffShift updatedShift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid data.", errors = ModelState.Values });
            }

            var existingShift = await _dbContext.StaffShifts.FindAsync(id);
            if (existingShift == null)
            {
                return NotFound(new { success = false, message = "Shift not found." });
            }

            if (!updatedShift.IsValidShift())
            {
                return BadRequest(new { success = false, message = "Start time must be before end time." });
            }

            if (!updatedShift.IsShiftInFuture())
            {
                return BadRequest(new { success = false, message = "Shift date cannot be in the past." });
            }

            // Update fields
            existingShift.UserId = updatedShift.UserId;
            existingShift.StartTime = updatedShift.StartTime;
            existingShift.EndTime = updatedShift.EndTime;
            existingShift.ShiftDate = updatedShift.ShiftDate;

            await _dbContext.SaveChangesAsync();
            return Ok(existingShift);
        }

        // DELETE: api/StaffShift/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var shift = await _dbContext.StaffShifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound(new { success = false, message = "Shift not found." });
            }

            _dbContext.StaffShifts.Remove(shift);
            await _dbContext.SaveChangesAsync();
            return Ok(new { success = true, message = "Shift deleted successfully." });
        }
    }
}
