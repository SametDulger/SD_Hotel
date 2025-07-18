using System;

namespace SD_Hotel.Application.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string PackageType { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string SpecialRequests { get; set; }
        public string Notes { get; set; }
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
        public string PackageType { get; set; }
        public string SpecialRequests { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateReservationDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string PackageType { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string SpecialRequests { get; set; }
        public string Notes { get; set; }
    }

    public class ReservationSearchDto
    {
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string CustomerName { get; set; }
        public string RoomType { get; set; }
        public int? NumberOfGuests { get; set; }
        public string PackageType { get; set; }
        public string Status { get; set; }
    }
} 