using System.ComponentModel.DataAnnotations;

namespace thoeun_coffee.Models
{
    public class StaffShift
    {
        [Key]
        public int ShiftId { get; set; }
        public int? UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ShiftDate { get; set; }

        public User? User { get; set; }
    }
}
