﻿using System.ComponentModel.DataAnnotations;

namespace thoeun_coffee.Models
{
    public class StaffShift
    {
        [Key]
        public int ShiftId { get; set; }

        public int? UserId { get; set; }

        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        public DateTime ShiftDate { get; set; }

        public User? User { get; set; }

        // Custom validation to ensure StartTime is before EndTime
        public bool IsValidShift()
        {
            return StartTime < EndTime;
        }

        // Custom validation to ensure ShiftDate is not in the past
        public bool IsShiftInFuture()
        {
            return ShiftDate >= DateTime.Now.Date;
        }
    }
}


// {
//   "userId": 1,
//   "startTime": "2024-06-22T09:00:00",
//   "endTime": "2024-06-22T17:00:00",
//   "shiftDate": "2025-06-22"
// }
