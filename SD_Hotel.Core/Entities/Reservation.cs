using System;

namespace SD_Hotel.Core.Entities
{
    public class Reservation : BaseEntity
    {
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public PackageType PackageType { get; set; }
        public decimal TotalPrice { get; set; }
        public ReservationStatus Status { get; set; }
        public string SpecialRequests { get; set; } = string.Empty;
        public DateTime? ConfirmationDate { get; set; }
        public bool IsEarlyReservation { get; set; } = false;
        public decimal? EarlyReservationDiscount { get; set; }
        
        public virtual Customer Customer { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }

    public enum PackageType
    {
        RoomOnly = 1,
        HalfBoard = 2,
        FullBoard = 3,
        AllInclusive = 4
    }

    public enum ReservationStatus
    {
        Pending = 1,
        Confirmed = 2,
        CheckedIn = 3,
        CheckedOut = 4,
        Cancelled = 5
    }
} 