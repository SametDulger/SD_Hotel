using Microsoft.EntityFrameworkCore;
using SD_Hotel.Core.Entities;
using SD_Hotel.Core.Repositories;
using SD_Hotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SD_Hotel.Infrastructure.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(SD_HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByCustomerAsync(int customerId)
        {
            return await _dbSet.Where(r => r.CustomerId == customerId && r.IsActive)
                              .Include(r => r.Room)
                              .OrderByDescending(r => r.CreatedDate)
                              .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByRoomAsync(int roomId)
        {
            return await _dbSet.Where(r => r.RoomId == roomId && r.IsActive)
                              .Include(r => r.Customer)
                              .OrderByDescending(r => r.CreatedDate)
                              .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet.Where(r => r.IsActive &&
                                          ((r.CheckInDate >= startDate && r.CheckInDate <= endDate) ||
                                           (r.CheckOutDate >= startDate && r.CheckOutDate <= endDate)))
                              .Include(r => r.Customer)
                              .Include(r => r.Room)
                              .OrderBy(r => r.CheckInDate)
                              .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByStatusAsync(ReservationStatus status)
        {
            return await _dbSet.Where(r => r.Status == status && r.IsActive)
                              .Include(r => r.Customer)
                              .Include(r => r.Room)
                              .OrderBy(r => r.CheckInDate)
                              .ToListAsync();
        }

        public async Task<Reservation?> GetReservationWithDetailsAsync(int id)
        {
            return await _dbSet.Where(r => r.Id == id && r.IsActive)
                              .Include(r => r.Customer)
                              .Include(r => r.Room)
                              .FirstOrDefaultAsync();
        }

        public async Task<bool> HasConflictingReservationAsync(int roomId, DateTime checkIn, DateTime checkOut, int? excludeReservationId = null)
        {
            var query = _context.Reservations.Where(r => r.RoomId == roomId && 
                                                        r.Status != ReservationStatus.Cancelled && 
                                                        r.IsActive);

            if (excludeReservationId.HasValue)
            {
                query = query.Where(r => r.Id != excludeReservationId.Value);
            }

            return await query.AnyAsync(r => (r.CheckInDate <= checkIn && r.CheckOutDate > checkIn) ||
                                            (r.CheckInDate < checkOut && r.CheckOutDate >= checkOut) ||
                                            (r.CheckInDate >= checkIn && r.CheckOutDate <= checkOut));
        }

        public async Task<IEnumerable<Reservation>> GetTodayCheckInsAsync()
        {
            var today = DateTime.Today;
            return await _dbSet.Where(r => r.CheckInDate.Date == today && r.IsActive)
                              .Include(r => r.Customer)
                              .Include(r => r.Room)
                              .OrderBy(r => r.CheckInDate)
                              .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetTodayCheckOutsAsync()
        {
            var today = DateTime.Today;
            return await _dbSet.Where(r => r.CheckOutDate.Date == today && r.IsActive)
                              .Include(r => r.Customer)
                              .Include(r => r.Room)
                              .OrderBy(r => r.CheckOutDate)
                              .ToListAsync();
        }
    }
} 