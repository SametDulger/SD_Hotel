using System.Collections.Generic;

namespace SD_Hotel.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsLoyaltyMember { get; set; } = false;
        public int LoyaltyPoints { get; set; } = 0;
        
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public virtual ICollection<ExtraExpense> ExtraExpenses { get; set; } = new List<ExtraExpense>();
    }
} 