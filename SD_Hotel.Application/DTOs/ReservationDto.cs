using System;

namespace SD_Hotel.Application.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string PackageType { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public string SpecialRequests { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime? ConfirmationDate { get; set; }
        public bool IsEarlyReservation { get; set; }
        public decimal? EarlyReservationDiscount { get; set; }
    }

    public class CreateReservationDto
    {
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string PackageType { get; set; } = string.Empty;
        public string SpecialRequests { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }

    public class UpdateReservationDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string PackageType { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public string SpecialRequests { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }

    public class ReservationSearchDto
    {
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public int? NumberOfGuests { get; set; }
        public string PackageType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
} 