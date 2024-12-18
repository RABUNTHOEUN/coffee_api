using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thoeun_coffee.Data;
using thoeun_coffee.Models;

namespace thoeun_coffee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffShiftController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StaffShiftController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StaffShift
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffShift>>> GetStaffShifts()
        {
            return await _context.StaffShifts.Include(s => s.User)
                                             .ToListAsync();
        }

        // GET: api/StaffShift/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffShift>> GetStaffShift(int id)
        {
            var staffShift = await _context.StaffShifts.Include(s => s.User)
                                                       .FirstOrDefaultAsync(s => s.ShiftId == id);

            if (staffShift == null)
            {
                return NotFound();
            }

            return staffShift;
        }

        // POST: api/StaffShift
        [HttpPost]
        public async Task<ActionResult<StaffShift>> PostStaffShift(StaffShift staffShift)
        {
            if (!staffShift.IsValidShift())
            {
                return BadRequest("StartTime must be earlier than EndTime.");
            }

            if (!staffShift.IsShiftInFuture())
            {
                return BadRequest("ShiftDate cannot be in the past.");
            }

            _context.StaffShifts.Add(staffShift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStaffShift", new { id = staffShift.ShiftId }, staffShift);
        }

        // PUT: api/StaffShift/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffShift(int id, StaffShift staffShift)
        {
            if (id != staffShift.ShiftId)
            {
                return BadRequest();
            }

            if (!staffShift.IsValidShift())
            {
                return BadRequest("StartTime must be earlier than EndTime.");
            }

            if (!staffShift.IsShiftInFuture())
            {
                return BadRequest("ShiftDate cannot be in the past.");
            }

            _context.Entry(staffShift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.StaffShifts.Any(s => s.ShiftId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/StaffShift/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffShift(int id)
        {
            var staffShift = await _context.StaffShifts.FindAsync(id);
            if (staffShift == null)
            {
                return NotFound();
            }

            _context.StaffShifts.Remove(staffShift);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
