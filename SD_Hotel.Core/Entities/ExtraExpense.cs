using System;

namespace SD_Hotel.Core.Entities
{
    public class ExtraExpense : BaseEntity
    {
        public int CustomerId { get; set; }
        public int? ReservationId { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime? PaymentDate { get; set; }
        
        public virtual Customer Customer { get; set; } = null!;
        public virtual Reservation? Reservation { get; set; }
    }

    public enum ExpenseType
    {
        Bar = 1,
        RoomService = 2,
        Spa = 3,
        Restaurant = 4,
        Laundry = 5,
        Other = 6
    }
} 