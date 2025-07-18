using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Hotel.Core.Entities;

namespace SD_Hotel.Core.Repositories
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservationsByCustomerAsync(int customerId);
        Task<IEnumerable<Reservation>> GetReservationsByRoomAsync(int roomId);
        Task<IEnumerable<Reservation>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Reservation>> GetReservationsByStatusAsync(ReservationStatus status);
        Task<Reservation> GetReservationWithDetailsAsync(int id);
        Task<bool> HasConflictingReservationAsync(int roomId, DateTime checkIn, DateTime checkOut, int? excludeReservationId = null);
        Task<IEnumerable<Reservation>> GetTodayCheckInsAsync();
        Task<IEnumerable<Reservation>> GetTodayCheckOutsAsync();
    }
} 