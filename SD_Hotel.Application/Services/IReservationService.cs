using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Hotel.Application.DTOs;

namespace SD_Hotel.Application.Services
{
    public interface IReservationService
    {
        Task<ReservationDto?> GetByIdAsync(int id);
        Task<IEnumerable<ReservationDto>> GetAllAsync();
        Task<ReservationDto?> CreateAsync(CreateReservationDto createReservationDto);
        Task<ReservationDto?> UpdateAsync(UpdateReservationDto updateReservationDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ReservationDto>> GetByCustomerAsync(int customerId);
        Task<IEnumerable<ReservationDto>> GetByRoomAsync(int roomId);
        Task<IEnumerable<ReservationDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<ReservationDto>> GetByStatusAsync(string status);
        Task<IEnumerable<ReservationDto>> GetTodayCheckInsAsync();
        Task<IEnumerable<ReservationDto>> GetTodayCheckOutsAsync();
        Task<bool> HasConflictingReservationAsync(int roomId, DateTime checkIn, DateTime checkOut, int? excludeReservationId = null);
        Task<decimal> CalculateTotalPriceAsync(int roomId, DateTime checkIn, DateTime checkOut, string packageType, int numberOfGuests);
    }
} 