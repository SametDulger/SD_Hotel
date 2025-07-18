using System;

namespace SD_Hotel.Core.Entities
{
    public class Overtime : BaseEntity
    {
        public int EmployeeId { get; set; }
        public DateTime OvertimeDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalPay { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; } = false;
        public DateTime? ApprovalDate { get; set; }
        
        public virtual Employee Employee { get; set; }
    }
} 