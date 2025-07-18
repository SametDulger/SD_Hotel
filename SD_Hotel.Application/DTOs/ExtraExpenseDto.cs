using System;

namespace SD_Hotel.Application.DTOs
{
    public class ExtraExpenseDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? ReservationId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string ExpenseType { get; set; }
        public DateTime ExpenseDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class CreateExtraExpenseDto
    {
        public int CustomerId { get; set; }
        public int? ReservationId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string ExpenseType { get; set; }
        public DateTime ExpenseDate { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime? PaymentDate { get; set; }
    }

    public class UpdateExtraExpenseDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? ReservationId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string ExpenseType { get; set; }
        public DateTime ExpenseDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
} 