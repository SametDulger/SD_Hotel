using System;
using System.Collections.Generic;

namespace SD_Hotel.Core.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public EmployeeRole Role { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal? MonthlySalary { get; set; }
        public DateTime HireDate { get; set; }
        public new bool IsActive { get; set; } = true;
        
        public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
        public virtual ICollection<Overtime> Overtimes { get; set; } = new List<Overtime>();
    }

    public enum EmployeeRole
    {
        Receptionist = 1,
        Housekeeper = 2,
        Chef = 3,
        Waiter = 4,
        Electrician = 5,
        ITManager = 6,
        Manager = 7
    }
} 