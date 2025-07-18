using System;
using System.Collections.Generic;

namespace SD_Hotel.Core.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public EmployeeRole Role { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal? MonthlySalary { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; } = true;
        
        public virtual ICollection<Shift> Shifts { get; set; }
        public virtual ICollection<Overtime> Overtimes { get; set; }
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