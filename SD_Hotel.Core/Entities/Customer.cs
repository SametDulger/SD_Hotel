using System.Collections.Generic;

namespace SD_Hotel.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsLoyaltyMember { get; set; } = false;
        public int LoyaltyPoints { get; set; } = 0;
        
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<ExtraExpense> ExtraExpenses { get; set; }
    }
} 