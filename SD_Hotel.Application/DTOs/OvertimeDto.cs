using System;

namespace SD_Hotel.Application.DTOs
{
    public class OvertimeDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime OvertimeDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalPay { get; set; }
        public string Reason { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class CreateOvertimeDto
    {
        public int EmployeeId { get; set; }
        public DateTime OvertimeDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Reason { get; set; } = string.Empty;
    }

    public class UpdateOvertimeDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime OvertimeDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalPay { get; set; }
        public string Reason { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
    }
} 