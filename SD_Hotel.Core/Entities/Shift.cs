using System;

namespace SD_Hotel.Core.Entities
{
    public class Shift : BaseEntity
    {
        public int EmployeeId { get; set; }
        public DateTime ShiftDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal HoursWorked { get; set; }
        public ShiftType ShiftType { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string Notes { get; set; }
        
        public virtual Employee Employee { get; set; }
    }

    public enum ShiftType
    {
        Morning = 1,    // 08:00-16:00
        Evening = 2,    // 16:00-00:00
        Night = 3       // 00:00-08:00
    }
} 